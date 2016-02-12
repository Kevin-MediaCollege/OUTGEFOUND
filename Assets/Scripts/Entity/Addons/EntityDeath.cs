using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EntityDeath : EntityAddon
{
	public delegate void OnDeath(Entity source);
	public event OnDeath onDeathEvent = delegate { };

	private Entity lastSource;

	private bool dead;

	protected void OnEnable()
	{
		Entity.Damagable.onDamageReceivedEvent += OnDamageReceived;
	}

	protected void OnDisable()
	{
		Entity.Damagable.onDamageReceivedEvent -= OnDamageReceived;
	}

	protected void LateUpdate()
	{
		if(dead)
		{
			return;
		}
		
		if(Entity.Health.Current <= 0)
		{
			dead = true;
			onDeathEvent(lastSource);
		}
	}

	private void OnDamageReceived(HitInfo hitInfo, int damage)
	{
		lastSource = hitInfo.Source;
	}
}