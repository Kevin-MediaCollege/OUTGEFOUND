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

		fireMode = ((FirearmUpgrade)Upgrade).FireModes;
	}

	protected void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			StartFire();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			StopFire();
		}
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

	protected override HitInfo ConstructHitInfo()
	{
		HitInfo hitInfo = new HitInfo(Entity, null);
		FirearmUpgrade fUpgrade = (FirearmUpgrade)Upgrade;

		// Apply bullet spread
		Vector2 rayPosition = new Vector2(0.5f, 0.5f);
		rayPosition += Random.insideUnitCircle * fUpgrade.BulletSpread;

		// Raycast from the camera forward
		Ray ray = Camera.main.ViewportPointToRay(rayPosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 1000))
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

	private void StartFire()
	{
		if(!firing)
		{
			firing = true;
			numShotsBurst = 0;
		}
	}

	private void StopFire()
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
}