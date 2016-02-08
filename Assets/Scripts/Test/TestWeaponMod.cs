using System;
using System.Collections;
using UnityEngine;

public class TestWeaponMod : BaseWeaponModifier
{
	[SerializeField] private float reloadTime;

	private bool reloaded = true;

	public override bool CanFire()
	{
		return reloaded;
	}

	public override void OnFire()
	{
		reloaded = false;

		StopCoroutine("Delay");
		StartCoroutine("Delay");
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(reloadTime);

		reloaded = true;
	}
}