using UnityEngine;
using System.Collections;
using System;

public class Damagable : EntityAddon
{
	public delegate void OnDamageReceived(DamageInfo info);
	public event OnDamageReceived onDamageReceivedEvent = delegate { };

	public void Damage(DamageInfo info)
	{
		onDamageReceivedEvent(info);
	}

#if UNITY_EDITOR
	[ContextMenu("Damage (1)")]
	private void Damage1()
	{
		Damage(new DamageInfo(Entity, Entity, Vector3.zero, Vector3.zero, Vector3.zero, 1));
	}

	[ContextMenu("Damage (5)")]
	private void Damage5()
	{
		Damage(new DamageInfo(Entity, Entity, Vector3.zero, Vector3.zero, Vector3.zero, 5));
	}

	[ContextMenu("Damage (10)")]
	private void Damage10()
	{
		Damage(new DamageInfo(Entity, Entity, Vector3.zero, Vector3.zero, Vector3.zero, 10));
	}

	[ContextMenu("Kill")]
	private void Kill()
	{
		Damage(new DamageInfo(Entity, Entity, Vector3.zero, Vector3.zero, Vector3.zero, 10000));
	}
#endif
}