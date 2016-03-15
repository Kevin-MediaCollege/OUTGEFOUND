using UnityEngine;

public class FirearmAimAI : WeaponAim
{
	public override Vector3 GetAimDirection(Vector3 position)
	{
		return (EnemyUtils.PlayerCenter - position).normalized;
	}
}