using UnityEngine;
using System.Collections;
using System;

public class HealthStoreItem : StoreItem
{
	public override void Purchase()
	{
		Entity player = EntityUtils.GetEntityWithTag("Player");

		int amount = player.Health.Starting - player.Health.Current;
		player.Health.Heal(amount);
	}
}