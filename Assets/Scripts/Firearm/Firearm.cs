using UnityEngine;
using System.Collections;

public class Firearm : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }

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

	[SerializeField] private float damage;
	[SerializeField] private float reloadSpeed;
	[SerializeField] private float bulletSpread;
	[SerializeField] private float range;

	[SerializeField] private int roundsPerMinute;

	private FirearmAimController aimController;
	private Magazine magazine;

	protected void Awake()
	{
		muzzleFlash.enabled = false;
	}

	protected void Start()
	{
		aimController = transform.GetComponentInParent<FirearmAimController>();
		magazine = transform.GetComponentInParent<Magazine>();
	}

	protected void OnEnable()
	{
		Entity.Events.AddListener<StartFireEvent>(OnStartFireEvent);
		Entity.Events.AddListener<StopFireEvent>(OnStopFireEvent);
	}

	protected void OnDisable()
	{
		Entity.Events.RemoveListener<StartFireEvent>(OnStartFireEvent);
		Entity.Events.RemoveListener<StopFireEvent>(OnStopFireEvent);
	}

	private void OnStartFireEvent(StartFireEvent evt)
	{
		StartCoroutine("Fire");
	}

	private void OnStopFireEvent(StopFireEvent evt)
	{
		StopCoroutine("Fire");
	}

	private IEnumerator Fire()
	{
		while(true)
		{
			if(magazine.Empty)
			{
				// TODO: Play mag empty sound clip
				yield break;
			}

			HitInfo hitInfo = ConstructHitInfo();
			GlobalEvents.Invoke(new FireEvent(this, hitInfo));

			if(hitInfo.Hit)
			{
				DamageInfo damageInfo = new DamageInfo(hitInfo, CalculateDamage(hitInfo));
				DamageEvent damageEvent = new DamageEvent(damageInfo);

				GlobalEvents.Invoke(damageEvent);
				Entity.Events.Invoke(damageEvent);
			}

			PostFire(hitInfo);

			yield return new WaitForSeconds(60f / roundsPerMinute);
		}
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
		Debug.DrawRay(barrel.position, direction * range, damagable ? Color.green : Color.red, 7);

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
		AudioChannel audioChannel = AudioManager.PlayAt(fireAudio, barrel.position);
		if(audioChannel != null)
		{
			audioChannel.Pitch = Random.Range(0.7f, 1.3f);
		}

		magazine.Fire();

		StopCoroutine("DisplayMuzzleFlash");
		StartCoroutine("DisplayMuzzleFlash");

		GlobalEvents.Invoke(new SpawnDecalEvent(hitInfo.Point, hitInfo.Normal));
	}

	private HitInfo ConstructHitInfo()
	{
		HitInfo hitInfo = new HitInfo(Entity);

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
