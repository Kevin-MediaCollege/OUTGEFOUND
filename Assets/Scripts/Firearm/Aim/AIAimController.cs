using UnityEngine;

public class AIAimController : FirearmAimController
{
	public override Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit)
	{
		Vector3 barrel = firearm.Barrel.position;
		Vector3 direction = (EnemyUtils.Player.transform.position - barrel).normalized;

		Ray ray = new Ray(barrel, direction);
		Physics.Raycast(ray, out hit, firearm.Range);

		return direction;
	}
}
