using UnityEngine;
using System.Collections;
using System;

public class Damagable : EntityAddon
{
	public delegate void OnDamageReceived(HitInfo hitInfo, int damage);
	public event OnDamageReceived onDamageReceivedEvent = delegate { };

	public void Damage(HitInfo hitInfo, int damage)
	{
		onDamageReceivedEvent(hitInfo, damage);
	}
}