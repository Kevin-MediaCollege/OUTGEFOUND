using UnityEngine;

public class AIAimController : FirearmAimController
{
	public override Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit)
	{
		Vector3 barrel = firearm.Barrel.position;
		Vector3 direction = (EnemyUtils.PlayerCenter - barrel).normalized;

		Ray ray = new Ray(barrel, direction);
		RaycastHit[] hits = Physics.RaycastAll(ray, firearm.Range, layerMask);
		hit = hits[0];

		foreach(RaycastHit h in hits)
		{
			Entity entity = h.collider.GetComponentInParent<Entity>();

			if(entity != null)
			{
				if(entity.HasTag("Player"))
				{
					hit = h;
					break;
				}
			}
		}
		
		return direction;
	}
}
