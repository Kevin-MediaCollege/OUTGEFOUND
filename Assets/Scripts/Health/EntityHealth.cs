using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityHealth : MonoBehaviour
{
	public delegate void OnDamageTaken(HitInfo hitInfo);
	public event OnDamageTaken onDamageTakenEvent = delegate { };

	public delegate void OnDeath(HitInfo hitInfo);
	public event OnDeath onDeathEvent = delegate { };

	public Entity Entity { private set; get; }

	[SerializeField] private int startingHealth;

	private HashSet<IHealthModifier> healthModifiers = new HashSet<IHealthModifier>();
	
	private int health;

	protected void Awake()
	{
		Entity = GetComponentInParent<Entity>();
		health = startingHealth;
	}

	public void Damage(HitInfo hitInfo)
	{
		if(health <= 0)
		{
			return;
		}

		foreach(IHealthModifier modifier in healthModifiers)
		{
			hitInfo = modifier.OnDamageReceived(hitInfo);
		}

		// Make sure health doesn't go below zero
		hitInfo.damage = Mathf.Min(health, hitInfo.damage);

		health -= hitInfo.damage;
		onDamageTakenEvent(hitInfo);

		// Send an event to let others know the entity has died
		if(health <= 0)
		{
			onDeathEvent(hitInfo);
		}
	}

	public void AddHealthModifier(IHealthModifier modifier)
	{
		healthModifiers.Add(modifier);
	}

	public void RemoveHealthModifier(IHealthModifier modifier)
	{
		healthModifiers.Remove(modifier);
	}
}