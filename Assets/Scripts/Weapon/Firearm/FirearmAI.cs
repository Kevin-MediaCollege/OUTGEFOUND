using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FirearmAI : Firearm
{
	protected override HitInfo GetHitInfo(Vector3 direction)
	{
		Ray ray = new Ray(Position, direction);
		RaycastHit[] hits = Physics.RaycastAll(ray, range, layers);
		IEnumerable<RaycastHit> query = hits.OrderBy(hit => hit.distance);

		foreach(RaycastHit hit in query)
		{
			if(hit.collider != null)
			{
				Entity entity = hit.collider.GetComponentInParent<Entity>();

				if(entity != null && entity.HasTag("Enemy"))
				{
					continue;
				}

				Debug.DrawRay(Position, direction * hit.distance, entity != null ? Color.green : Color.red, 3);
				return new HitInfo(Wielder, entity, hit.point, hit.normal, tag);
			}
		}

		Debug.DrawRay(Position, direction * range, Color.red, 3);
		return new HitInfo(Wielder, null, Vector3.zero, Vector3.zero, "Untagged");
	}
}