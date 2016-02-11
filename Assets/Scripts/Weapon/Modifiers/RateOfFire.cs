using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateOfFire : BaseWeaponModifier
{
	[SerializeField] private int roundsPerMinute;
	
	private bool canFire = true;

	public override bool CanFire()
	{
		return canFire;
	}

	public override void OnFire(ref DamageInfo info)
	{
		canFire = false;
		StartCoroutine("Delay");
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(60f / roundsPerMinute);

		canFire = true;
	}
}