using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EntityHealth : MonoBehaviour, IEntityInjector
{
	public delegate void OnDeath(ShotInfo shotInfo);
	public event OnDeath onDeathEvent = delegate { };

	public Entity Entity { private set; get; }

	[SerializeField] private int startingHealth;

	private HashSet<IHealthModifier> healthModifiers;

	private Damagable damagable;

	private int health;

	protected void Awake()
	{
		if(healthModifiers == null)
		{
			healthModifiers = new HashSet<IHealthModifier>();
		}

		health = startingHealth;
	}

	protected void OnEnable()
	{
		if(damagable != null)
		{
			damagable.onDamageReceivedEvent += OnDamageReceived;
		}
	}

	protected void OnDisable()
	{
		damagable.onDamageReceivedEvent -= OnDamageReceived;
	}

	public void Damage(int damage)
	{
		ShotInfo shotInfo = new ShotInfo(Entity, damagable, Vector3.zero, Vector3.zero, Vector3.zero, damage);
		OnDamageReceived(shotInfo);
	}

	public void Kill()
	{
		Damage(health);
	}

	#region Modifier Callbacks
	public void AddHealthModifier(IHealthModifier modifier)
	{
		if(healthModifiers == null)
		{
			healthModifiers = new HashSet<IHealthModifier>();
		}

		healthModifiers.Add(modifier);
	}

	public void RemoveHealthModifier(IHealthModifier modifier)
	{
		healthModifiers.Remove(modifier);
	}
	#endregion

	public void RegisterEntity(Entity entity)
	{
		Entity = entity;
		damagable = entity.GetComponentInChildren<Damagable>();
		damagable.onDamageReceivedEvent += OnDamageReceived;
	}

	private void OnDamageReceived(ShotInfo shotInfo)
	{
		if(health <= 0)
		{
			return;
		}

		foreach(IHealthModifier modifier in healthModifiers)
		{
			modifier.OnDamageReceived(ref shotInfo);
		}

		// Make sure health doesn't go below zero
		shotInfo.Damage = Mathf.Min(health, shotInfo.Damage);

		health -= shotInfo.Damage;

		// Send an event to let others know the entity has died
		if(health <= 0)
		{
			onDeathEvent(shotInfo);
		}
	}
}