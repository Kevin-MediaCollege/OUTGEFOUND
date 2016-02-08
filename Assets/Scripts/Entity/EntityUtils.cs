using System;
using System.Collections.Generic;
using UnityEngine;

public static class EntityUtils
{
	public static Rigidbody Rigidbody(this Entity entity)
	{
		return entity.GetComponent<Rigidbody>();
	}

	public static EntityMovement Movement(this Entity entity)
	{
		return entity.GetComponent<EntityMovement>();
	}

	public static Transform Eyes(this Entity entity)
	{
		Transform eyes = entity.transform.Find("Eyes");

		if(eyes == null)
		{
			Debug.LogError("[EntityUtils] Entity doesn't have eyes!", entity);
		}

		return eyes;
	}

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

		return null;
	}
}