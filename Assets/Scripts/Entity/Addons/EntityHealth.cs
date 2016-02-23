using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EntityHealth : IDependency
{
	public delegate void OnHit(DamageInfo damageInfo);
	public event OnHit onHitEvent = delegate { };

	public delegate void OnDeath(DamageInfo damageInfo);
	public event OnDeath onDeathEvent = delegate { };

	private Dictionary<Entity, EntityHealthData> data;

	public EntityHealth()
	{
		data = new Dictionary<Entity, EntityHealthData>();
	}

	public void Add(Entity entity, EntityHealthData healthData)
	{
		data.Add(entity, healthData);
	}

	public void Remove(Entity entity)
	{
		data.Remove(entity);
	}

	public void Heal(Entity entity)
	{
		if(CanDamage(entity))
		{
			EntityHealthData d = data[entity];
			d.CurrentHealth = d.StartingHealth;
		}
	}

	public void Damage(DamageInfo damageInfo)
	{
		if(CanDamage(damageInfo.Target))
		{
			EntityHealthData d = data[damageInfo.Target];

			if(d.CurrentHealth > 0)
			{
				d.CurrentHealth -= damageInfo.Damage;

				onHitEvent(damageInfo);

				if(d.CurrentHealth <= 0)
				{
					onDeathEvent(damageInfo);
				}
			}
		}
	}

	public bool CanDamage(Entity entity)
	{
		return data.ContainsKey(entity);
	}
}