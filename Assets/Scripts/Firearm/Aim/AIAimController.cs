using UnityEngine;

/// <summary>
/// AI aim controller, makes the AI aim at the player
/// </summary>
public class AIAimController : FirearmAimController
{
	public override Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit, float recoilOffset)
	{
		Vector3 barrel = firearm.Barrel.position;
		Vector3 direction = (EnemyUtils.PlayerCenter - barrel).normalized;

		// Apply bullet spread
		Vector3 spread = Random.insideUnitCircle;
		direction += spread * firearm.BulletSpread;

		// Apply recoil offset
		direction.y += recoilOffset;

		Ray ray = new Ray(barrel, direction);
		RaycastHit[] hits = Physics.RaycastAll(ray, firearm.Range, layerMask);
		hit = new RaycastHit();
		
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
