using UnityEngine;

public class PlayerAimController : FirearmAimController
{
	public override Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit)
	{
		Vector2 crosshair = new Vector2(0.5f, 0.5f);

		Ray ray = Camera.main.ViewportPointToRay(crosshair);

		if(Physics.Raycast(ray, out hit, firearm.Range))
		{
			Vector3 barrel = firearm.Barrel.position;
			Vector3 direction = (hit.point - barrel).normalized;

			ray = new Ray(barrel, hit.point - barrel);

			if(Physics.Raycast(ray, out hit, firearm.Range))
			{
				return direction;
			}
		}

		return ray.direction;
	}
}
