using System;
using System.Collections;
using UnityEngine;

public class TestDamageMod : BaseDamageModifier
{
	public override HitInfo Modify(HitInfo hitInfo)
	{
		hitInfo.damage *= 3;
		return hitInfo;
	}
}