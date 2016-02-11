using UnityEngine;
using System.Collections;
using System;

public class DestroyOnDeath : MonoBehaviour, IEntityInjector
{
	private Entity entity;

	protected void OnEnable()
	{
		entity.Death.onDeathEvent += OnDeathEvent;
	}

	protected void OnDisable()
	{
		entity.Death.onDeathEvent -= OnDeathEvent;
	}

	public void RegisterEntity(Entity entity)
	{
		this.entity = entity;
	}

	private void OnDeathEvent(Entity source)
	{
		Destroy(entity.gameObject);
	}
}