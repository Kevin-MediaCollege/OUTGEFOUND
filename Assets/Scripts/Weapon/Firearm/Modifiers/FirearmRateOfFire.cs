using UnityEngine;
using System.Collections;
using System;

public class FirearmRateOfFire : WeaponModifier
{
	private bool canFire = true;

	public override void OnFire(HitInfo hitInfo)
	{
		StartCoroutine(RateOfFire());
	}

	public override bool CanFire()
	{
		return canFire;
	}

	private IEnumerator RateOfFire()
	{
		canFire = false;

		yield return new WaitForSeconds(60f / weapon.Upgrade.RateOfFire);
		
		canFire = true;
	}
}