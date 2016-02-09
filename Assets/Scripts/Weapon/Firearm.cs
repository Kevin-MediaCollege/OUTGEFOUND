using UnityEngine;
using System.Collections;

public class Firearm : Weapon
{
	[SerializeField] private Transform barrel;

	protected override bool GetshotInfo(out ShotInfo shotInfo)
	{
		Ray ray = new Ray(barrel.position, barrel.forward);
		RaycastHit hit;

		shotInfo = new ShotInfo(Wielder, null, ray.direction, Vector3.zero, Vector3.zero, baseDamage);

		if(Raycast(ray, out hit))
		{
			shotInfo.Target = hit.collider.GetComponentInParent<Damagable>();
			shotInfo.Point = hit.point;
			shotInfo.Normal = hit.normal;

			return shotInfo.HitDamagable;
		}

		return false;
	}

	private bool Raycast(Ray ray, out RaycastHit hit)
	{
		if(Physics.Raycast(ray, out hit, 100))
		{
			Debug.DrawRay(barrel.position, barrel.forward * hit.distance, Color.green, 7);
			return true;
		}

		Debug.DrawRay(barrel.position, barrel.forward * 100, Color.red, 7);
		return false;
	}
}