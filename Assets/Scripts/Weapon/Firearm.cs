using System;
using System.Collections;
using UnityEngine;

public class Firearm : Weapon
{
	public delegate void OnFire(HitInfo hitInfo);
	public event OnFire onFireEvent = delegate { };

	[SerializeField] private Transform barrel;

	protected override void Fire()
	{
		HitInfo? hitInfo = GenerateHitInfo();
		if(hitInfo != null)
		{
			onFireEvent(hitInfo.Value);

			// Let the target know it's been hit
			hitInfo.Value.target.AddonHealth.Damage(hitInfo.Value);
		}
	}

	private HitInfo? GenerateHitInfo()
	{
		Ray ray = new Ray(barrel.position, barrel.forward);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 100))
		{
			Debug.DrawRay(barrel.position, barrel.forward * hit.distance, Color.green, 7);

			Vector3 point = hit.point;
			Vector3 normal = hit.normal;

			Entity target = hit.collider.GetComponentInParent<Entity>();

			return ApplyDamageModifiers(new HitInfo(Entity, target, point, normal, 1));
		}
		else
		{
			Debug.DrawRay(barrel.position, barrel.forward * 100, Color.red, 7);
		}

		return null;
	}
}