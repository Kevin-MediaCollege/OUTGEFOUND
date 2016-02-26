using System;
using System.Collections.Generic;

public class Currency : IDependency
{
	public int Amount { set; get; }

	public Currency()
	{
		Amount = 0;
	}

	public void Create()
	{
		GlobalEvents.AddListener<EntityDeathEvent>(OnEntityDeathEvent);
	}

	public void Destroy()
	{
		GlobalEvents.RemoveListener<EntityDeathEvent>(OnEntityDeathEvent);
	}

	private void OnEntityDeathEvent(EntityDeathEvent evt)
	{
		Entity target = evt.DamageInfo.Hit.Target;

		// Reset if the player dies
		if(target.HasTag("Player"))
		{
			Amount = 0;
		}
		// Add currency if the target is an enemy
		else if(target.HasTag("Enemy"))
		{
			EnemyValue value = target.GetComponent<EnemyValue>();
			Amount += value.Value;
		}

		UnityEngine.Debug.Log(Amount);
	}
}