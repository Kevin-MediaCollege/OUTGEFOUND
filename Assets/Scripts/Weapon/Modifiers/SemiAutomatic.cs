using System;
using System.Collections.Generic;

public class SemiAutomatic : BaseWeaponModifier
{
	public override void OnFire(ref ShotInfo shotInfo)
	{
		weapon.StopFire();
	}
}