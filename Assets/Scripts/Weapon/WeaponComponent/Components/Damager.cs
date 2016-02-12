using UnityEngine;
using System.Collections;

public class Damager : WeaponComponent
{
	[SerializeField] private int damage;

	public override void Fire(HitInfo hitInfo)
	{
		if(hitInfo.Hit)
		{
			hitInfo.Target.Damagable.Damage(hitInfo, damage);
		}
	}
}