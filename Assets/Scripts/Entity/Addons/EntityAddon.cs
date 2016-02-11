using UnityEngine;
using System.Collections;
using System;

public abstract class EntityAddon : MonoBehaviour, IEntityInjector
{
	protected Entity Entity { private set; get; }

	public void RegisterEntity(Entity entity)
	{
		Entity = entity;
	}
}