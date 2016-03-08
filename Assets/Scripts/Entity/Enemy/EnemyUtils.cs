using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of utilities for enemies
/// </summary>
public static class EnemyUtils
{
	public static IEnumerable<Entity> Allies
	{
		get
		{
			return EntityUtils.GetEntitiesWithTag("Enemy");
		}
	}

	private static Entity player;
	public static Entity Player
	{
		get
		{
			if(player == null)
			{
				player = EntityUtils.GetEntityWithTag("Player");
			}

			return player;
		}
	}

	public static Vector3 PlayerCenter
	{
		get
		{
			return Player.transform.Find("AI Target").position;
		}
	}
}