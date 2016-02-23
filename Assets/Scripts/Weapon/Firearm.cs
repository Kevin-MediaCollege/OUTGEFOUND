using System;
using System.Collections;
using UnityEngine;

public class Firearm : Weapon
{
	[Flags]
	public enum FireMode
	{
		Automatic		= 1,
		Burst			= 2,
		SemiAutomatic	= 4
	}

	public Vector3 BarrelPosition
	{
		get
		{
			return barrel.position;
		}
	}

	[SerializeField] private Transform barrel;
	[SerializeField] private FireMode fireModes;
	[SerializeField] private Magazine magazine;

	[SerializeField] private int shotsPerBurst;

	private FireMode currentFireMode;

	private int currentShotCount;

	protected void OnEnable()
	{
		if(!SetFireMode(FireMode.Automatic))
		{
			if(!SetFireMode(FireMode.Burst))
			{
				if(!SetFireMode(FireMode.SemiAutomatic))
				{
					Debug.LogError("[Firearm] No fire modes!");
					currentFireMode = FireMode.Automatic;
				}
			}
		}

		onFireEvent += OnFireEvent;
	}

	protected void OnDisable()
	{
		onFireEvent -= OnFireEvent;
	}

	public override void StartFire()
	{
		base.StartFire();

		if(currentFireMode == FireMode.Burst)
		{
			currentShotCount = 0;
		}
	}

	public override void StopFire()
	{
		if(currentFireMode == FireMode.Burst)
		{
			if(currentShotCount >= shotsPerBurst)
			{
				base.StopFire();
			}
		}
		else
		{
			base.StopFire();
		}
	}

	public void SwitchFireMode()
	{
		if(currentFireMode == FireMode.Automatic)
		{
			if(!SetFireMode(FireMode.Burst))
			{
				SetFireMode(FireMode.SemiAutomatic);
			}
		}
		else if(currentFireMode == FireMode.Burst)
		{
			if(!SetFireMode(FireMode.SemiAutomatic))
			{
				SetFireMode(FireMode.Automatic);
			}
		}
		else if(currentFireMode == FireMode.SemiAutomatic)
		{
			if(!SetFireMode(FireMode.Automatic))
			{
				SetFireMode(FireMode.Burst);
			}
		}
	}

	public bool SetFireMode(FireMode fireMode)
	{
		if(HasFireMode(fireMode))
		{
			currentFireMode = fireMode;
			Debug.Log("[Firearm] Set fire mode to: " + fireMode);

			return true;
		}

		return false;
	}

	protected virtual void OnFireEvent(HitInfo hitInfo)
	{
		if(!magazine.Fire(this))
		{
			base.StopFire();
		}

		if(currentFireMode == FireMode.Burst)
		{
			currentShotCount++;
			if(currentShotCount >= shotsPerBurst)
			{
				StopFire();
			}
		}
		else if(currentFireMode == FireMode.SemiAutomatic)
		{
			StopFire();
		}
	}

	protected override HitInfo GetHitInfo()
	{
		HitInfo hitInfo = new HitInfo(Entity, null, Vector3.zero, Vector3.zero, Vector3.zero);

		// Raycast from the camera forward
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 1000))
		{
			// Raycast from the barrel to the hit point
			Vector3 direction = (hit.point - barrel.position).normalized;
			ray = new Ray(barrel.position, hit.point - barrel.position);

			if(Physics.Raycast(ray, out hit, 1000))
			{
				Debug.DrawRay(barrel.position, direction * hit.distance, Color.green, 7);
				
				hitInfo.Direction = ray.direction;
				hitInfo.Target = hit.collider.GetComponentInParent<Entity>();
				hitInfo.Point = hit.point;
				hitInfo.Normal = hit.normal;
			}
			else
			{
				Debug.DrawRay(barrel.position, direction * 1000, Color.red, 7);
			}
		}

		return hitInfo;
	}

	private bool HasFireMode(FireMode fireMode)
	{
		return (fireModes & fireMode) == fireMode;
	}
}