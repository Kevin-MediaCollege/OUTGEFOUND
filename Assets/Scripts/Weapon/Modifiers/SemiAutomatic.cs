using System;
using System.Collections.Generic;

public class SemiAutomatic : BaseWeaponModifier
{
	public override void OnFire(ref DamageInfo info)
	{
		weapon.StopFire();
	}
}