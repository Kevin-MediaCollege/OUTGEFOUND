using System;
using System.Collections.Generic;
using UnityEngine;

public static class EntityUtils
{
	public static IEnumerable<Entity> GetEntitiesWithTag(string tag)
	{
		HashSet<Entity> result = new HashSet<Entity>();

		foreach(Entity entity in Entity.All)
		{
			if(entity.HasTag(tag))
			{
				result.Add(entity);
			}
		}

		return result;
	}

	public static Entity GetEntityWithTag(string tag)
	{
		foreach(Entity entity in Entity.All)
		{
			if(entity.HasTag(tag))
			{
				return entity;
			}
		}

		Debug.LogError("[EntityUtils] No entity with tag: " + tag + " found");
		return null;
	}
}