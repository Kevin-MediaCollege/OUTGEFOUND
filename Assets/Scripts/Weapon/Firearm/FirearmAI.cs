using UnityEngine;
using System.Collections;

public class FirearmAI : Firearm
{
	protected override HitInfo GetHitInfo(Vector3 direction)
	{
		Ray ray = new Ray(Position, direction);
		RaycastHit[] hits = Physics.RaycastAll(ray, range);

		foreach(RaycastHit hit in hits)
		{
			Entity target = null;
			string tag = "Untagged";

			if(hit.collider != null)
			{
				target = hit.collider.GetComponentInParent<Entity>();
				tag = hit.collider.tag;

				if(target != null && target.HasTag("Enemy"))
				{
					continue;
				}
			}

			Debug.DrawRay(Position, direction * hit.distance, target != null ? Color.green : Color.red, 3);
			return new HitInfo(Wielder, target, hit.point, hit.normal, tag);
		}

		Debug.DrawRay(Position, direction * range, Color.red, 3);
		return new HitInfo(Wielder, null, Vector3.zero, Vector3.zero, "Untagged");
	}
}