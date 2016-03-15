using UnityEngine;

public class FirearmAimPlayer : WeaponAim
{
	public override Vector3 GetAimDirection(Vector3 position)
	{
		Vector2 crosshair = new Vector2(0.5f, 0.5f);

		Ray ray = Camera.main.ViewportPointToRay(crosshair);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			Vector3 direction = (hit.point - position).normalized;
			
			return direction;
		}
		
		// Should never happen
		return ray.direction;
	}
}