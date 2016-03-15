using UnityEngine;

/// <summary>
/// Player aim controller, makes the player aim at the crosshair
/// </summary>
public class PlayerAimController : FirearmAimController
{
	public override Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit, float recoilOffset)
	{
		Vector2 crosshair = new Vector2(0.5f, 0.5f);

		Ray ray = Camera.main.ViewportPointToRay(crosshair);

		if(Physics.Raycast(ray, out hit, firearm.Range))
		{
			Vector3 barrel = firearm.Barrel.position;
			Vector3 direction = (hit.point - barrel).normalized;

			// Apply recoil offset
			direction.y += recoilOffset;

			// Apply bullet spread
			Vector3 spread = Random.insideUnitCircle * firearm.BulletSpread;

			CharacterController cc = firearm.Wielder.GetComponent<CharacterController>();
			if(cc != null)
			{
				spread *= Mathf.Clamp(cc.velocity.magnitude, 1, 0.5f);
			}
			
			ray = new Ray(barrel, direction + spread);

			if(Physics.Raycast(ray, out hit, firearm.Range, layerMask))
			{
				return direction;
			}
		}

		return ray.direction;
	}
}
