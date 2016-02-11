using System;
using System.Collections.Generic;
using UnityEngine;

public class BurstFirearm : Firearm
{
	[SerializeField] private int shotsPerBurst;

	private int currentShots;

	public override void StartFire()
	{
		if(!CanFire())
		{
			return;
		}

		base.StartFire();

		currentShots = 0;
	}

	public override void StopFire(bool force = false)
	{
		if(force || currentShots >= shotsPerBurst)
		{
			base.StopFire();
		}
	}

	protected override void OnFire()
	{
		base.OnFire();

		currentShots++;
		StopFire();
	}
}