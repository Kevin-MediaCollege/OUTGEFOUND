using System;
using System.Collections;
using UnityEngine;

public class TestHealthMod : BaseHealthModifier
{
	public override HitInfo OnDamageReceived(HitInfo hitInfo)
	{
		hitInfo.damage = 0;
		return hitInfo;
	}
}