using UnityEngine;

public class Firearm : Weapon
{
	[SerializeField] private Magazine magazine;

	private FireMode fireMode;
	private Transform barrel;

	private bool firing;

	private int numShotsBurst;

	protected override void Awake()
	{
		base.Awake();

		SwitchFireMode();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		Entity.Events.AddListener<SwitchFireModeEvent>(OnSwitchFireModeEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		Entity.Events.RemoveListener<SwitchFireModeEvent>(OnSwitchFireModeEvent);
	}

	protected void FixedUpdate()
	{
		if(firing)
		{
			if(magazine.Current > 0)
			{
				if(Fire())
				{
					if(fireMode == FireMode.Burst)
					{
						numShotsBurst++;

						if(numShotsBurst >= ((FirearmUpgrade)Upgrade).ShotsPerBurst)
						{
							firing = false;
						}
					}
					else if(fireMode == FireMode.SemiAutomatic)
					{
						firing = false;
					}
				}
			}
			else
			{
				firing = false;
			}
		}
	}

	public override void StartFire()
	{
		if(!firing)
		{
			firing = true;
			numShotsBurst = 0;
		}
	}

	public override void StopFire()
	{
		if(firing)
		{
			switch(fireMode)
			{
			case FireMode.Automatic:
				firing = false;
				break;
			}
		}
	}

	public void SwitchFireMode()
	{
		if(fireMode == FireMode.Automatic)
		{
			if(!SetFireMode(FireMode.Burst))
			{
				SetFireMode(FireMode.SemiAutomatic);
			}
		}
		else if(fireMode == FireMode.Burst)
		{
			if(!SetFireMode(FireMode.SemiAutomatic))
			{
				SetFireMode(FireMode.Automatic);
			}
		}
		else if(fireMode == FireMode.SemiAutomatic)
		{
			if(!SetFireMode(FireMode.Automatic))
			{
				SetFireMode(FireMode.Burst);
			}
		}
		else
		{
			if(!SetFireMode(FireMode.Automatic) && !SetFireMode(FireMode.Burst) && !SetFireMode(FireMode.SemiAutomatic))
			{
				Debug.LogError("No available fire modes");
			}
		}
	}

	protected override HitInfo ConstructHitInfo()
	{
		HitInfo hitInfo = new HitInfo(Entity, null);
		FirearmUpgrade fUpgrade = (FirearmUpgrade)Upgrade;

		// Apply bullet spread
		Vector3 rayDirection = barrel.forward;
		rayDirection += Random.insideUnitSphere * fUpgrade.BulletSpread;
		
		Ray ray = new Ray(barrel.position, rayDirection);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, fUpgrade.MaxRange))
		{
			// Raycast from the barrel to the hit point
			Vector3 direction = (hit.point - barrel.position).normalized;
			ray = new Ray(barrel.position, hit.point - barrel.position);

			if(Physics.Raycast(ray, out hit, fUpgrade.MaxRange))
			{
				hitInfo.Direction = ray.direction;
				hitInfo.Target = hit.collider.GetComponentInParent<Entity>();
				hitInfo.Point = hit.point;
				hitInfo.Normal = hit.normal;
				hitInfo.Tag = hit.collider.tag;

				if(hitInfo.Hit)
				{
					Debug.DrawRay(barrel.position, direction * hit.distance, Color.green, 7);
				}
				else
				{
					Debug.DrawRay(barrel.position, direction * fUpgrade.MaxRange, Color.red, 7);
				}
			}
			else
			{
				Debug.DrawRay(barrel.position, direction * fUpgrade.MaxRange, Color.red, 7);
			}
		}

		return hitInfo;
	}
	protected override void SetUpgrade(WeaponUpgrade upgrade)
	{
		base.SetUpgrade(upgrade);

		barrel = Model.transform.Find("Barrel");
	}

	private bool SetFireMode(FireMode fireMode)
	{
		if(HasFireMode(fireMode))
		{
			this.fireMode = fireMode;
			Debug.Log(this.fireMode);
			return true;
		}

		return false;
	}

	private bool HasFireMode(FireMode fireMode)
	{
		return (((FirearmUpgrade)Upgrade).FireModes & fireMode) == fireMode;
	}

	private void OnSwitchFireModeEvent(SwitchFireModeEvent evt)
	{
		if(evt.Entity == Entity)
		{
			SwitchFireMode();
		}
	}
}