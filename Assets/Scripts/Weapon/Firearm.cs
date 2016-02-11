using UnityEngine;
using System.Collections;

public class Firearm : Weapon
{
	[SerializeField] private Transform barrel;
	
	protected override bool GetshotInfo(out DamageInfo info)
	{
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hit;

		info = new DamageInfo(Wielder, null, ray.direction, Vector3.zero, Vector3.zero, baseDamage);

		if(Raycast(ray, out hit))
		{
			info.Target = hit.collider.GetComponentInParent<Entity>();
			info.Point = hit.point;
			info.Normal = hit.normal;

			return info.Hit;
		}

		return false;
	}

	private bool Raycast(Ray ray, out RaycastHit hit)
	{
		if(Physics.Raycast(ray, out hit, 100))
		{
			Debug.DrawRay(barrel.position, ray.direction * hit.distance, Color.green, 7);
			return true;
		}

		Debug.DrawRay(barrel.position, ray.direction * 100, Color.red, 7);
		return false;
	}
}