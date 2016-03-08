using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firearm : MonoBehaviour
{
	public const int MAX_AUDIO_CHANNELS = 3;

	public Entity Wielder { private set; get; }

	public Transform Barrel
	{
		get
		{
			return barrel;
		}
	}

	public float Damage
	{
		get
		{
			return damage;
		}
	}

	public float ReloadSpeed
	{
		get
		{
			return reloadSpeed;
		}
	}

	public float BulletSpread
	{
		get
		{
			return bulletSpread;
		}
	}

	public float Range
	{
		get
		{
			return range;
		}
	}

	public int RoundsPerMinute
	{
		get
		{
			return roundsPerMinute;
		}
	}

	[SerializeField, EnumFlags] private FireMode fireModes;
	[SerializeField] private DamageMultiplier damageMultipliers;

	[SerializeField] private Transform barrel;
	[SerializeField] private AudioAsset fireAudio;
	[SerializeField] private SpriteRenderer muzzleFlash;

	[SerializeField] private AudioAsset reloadAudio;
	[SerializeField] private float reloadSpeed;

	[SerializeField] private AudioAsset clipEmptyAudio;

	[SerializeField] private float damage;	
	[SerializeField] private float bulletSpread;
	[SerializeField] private float range;

	[SerializeField] private int roundsPerMinute;

	private FirearmAimController aimController;
	private AudioManager audioManager;

	private Magazine magazine;
	private AmmoStockPile stockPile;

	private bool reloading;

	protected void Awake()
	{
		audioManager = Dependency.Get<AudioManager>();
		muzzleFlash.enabled = false;
	}

	protected void Start()
	{
		aimController = transform.GetComponentInParent<FirearmAimController>();

		magazine = transform.GetComponentInParent<Magazine>();
		stockPile = transform.GetComponentInParent<AmmoStockPile>();
	}

	protected void OnEnable()
	{
		if(Wielder == null)
		{
			Wielder = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
		}

		Wielder.Events.AddListener<StartFireEvent>(OnStartFireEvent);
		Wielder.Events.AddListener<StopFireEvent>(OnStopFireEvent);
		Wielder.Events.AddListener<ReloadEvent>(OnReloadEvent);
	}

	protected void OnDisable()
	{
		Wielder.Events.RemoveListener<StartFireEvent>(OnStartFireEvent);
		Wielder.Events.RemoveListener<StopFireEvent>(OnStopFireEvent);
		Wielder.Events.RemoveListener<ReloadEvent>(OnReloadEvent);
	}

	private void OnStartFireEvent(StartFireEvent evt)
	{
		StartCoroutine("Fire");
	}

	private void OnStopFireEvent(StopFireEvent evt)
	{
		StopCoroutine("Fire");
	}

	private void OnReloadEvent(ReloadEvent evt)
	{
		if(!reloading)
		{
			reloading = true;

			if(stockPile != null)
			{
				if(stockPile.Current <= 0)
				{
					reloading = false;
					return;
				}

				stockPile.Current += magazine.Remaining;
			}
			
			magazine.Remaining = 0;

			StartCoroutine("Reload");
		}
	}

	private IEnumerator Fire()
	{
		while(true)
		{
			if(magazine.Empty)
			{
				audioManager.PlayAt(clipEmptyAudio, barrel.position);
				yield break;
			}

			HitInfo hitInfo = ConstructHitInfo();
			GlobalEvents.Invoke(new FireEvent(this, hitInfo));

			if(hitInfo.Hit)
			{
				DamageInfo damageInfo = new DamageInfo(hitInfo, CalculateDamage(hitInfo));
				DamageEvent damageEvent = new DamageEvent(damageInfo);

				GlobalEvents.Invoke(damageEvent);
				hitInfo.Target.Events.Invoke(damageEvent);
			}

			PostFire(hitInfo);

			yield return new WaitForSeconds(60f / roundsPerMinute);
		}
	}

	private IEnumerator Reload()
	{
		int count = stockPile != null ? Mathf.Min(magazine.Capacity, stockPile.Current) : magazine.Capacity;

		audioManager.PlayAt(reloadAudio, barrel.position);

		yield return new WaitForSeconds(reloadSpeed);

		magazine.Remaining = count;

		if(stockPile != null)
		{
			stockPile.Current -= count;
		}

		reloading = false;
	}

	private IEnumerator DisplayMuzzleFlash()
	{
		Vector3 euler = muzzleFlash.transform.eulerAngles;
		euler.z = Random.Range(0f, 360f);
		muzzleFlash.transform.eulerAngles = euler;

		muzzleFlash.enabled = true;

		yield return new WaitForSeconds(0.05f);

		muzzleFlash.enabled = false;
	}

	private bool CalculateHit(out RaycastHit raycastHit, out Vector3 direction)
	{
		direction = aimController.GetAimDirection(this, out raycastHit);

		// Apply bullet spread
		Vector3 spread = Random.insideUnitCircle;
		direction += spread * bulletSpread;

		// Draw debug ray
		bool damagable = raycastHit.collider.GetComponentInParent<Damagable>() != null;
		Debug.DrawRay(barrel.position, direction * range, damagable ? Color.green : Color.red, 3);

		return damagable;
	}

	private float CalculateDamage(HitInfo hitInfo)
	{
		float damageToApply = damage;

		// Apply damage modifiers
		if(hitInfo.Tag == "Head")
		{
			damage *= damageMultipliers.Head;
		}
		else if(hitInfo.Tag == "Body")
		{
			damage *= damageMultipliers.Body;
		}
		else if(hitInfo.Tag == "Limb")
		{
			damage *= damageMultipliers.Limbs;
		}

		return damageToApply;
	}

	private void PostFire(HitInfo hitInfo)
	{
		AudioChannel channel = audioManager.PlayAt(fireAudio, barrel.position);
		if(channel != null)
		{
			channel.Pitch = Random.Range(0.7f, 1.3f);
		}

		magazine.Remaining--;

		StopCoroutine("DisplayMuzzleFlash");
		StartCoroutine("DisplayMuzzleFlash");

		GlobalEvents.Invoke(new SpawnDecalEvent(hitInfo.Point, hitInfo.Normal, hitInfo.Tag));
	}

	private HitInfo ConstructHitInfo()
	{
		HitInfo hitInfo = new HitInfo(Wielder);

		RaycastHit raycastHit;
		Vector3 direction;

		if(CalculateHit(out raycastHit, out direction))
		{
			hitInfo.Target = raycastHit.collider.GetComponentInParent<Entity>();
			hitInfo.Direction = direction;
			hitInfo.Point = raycastHit.point;
			hitInfo.Normal = raycastHit.normal;
			hitInfo.Tag = raycastHit.collider.tag;
		}

		return hitInfo;
	}
}
