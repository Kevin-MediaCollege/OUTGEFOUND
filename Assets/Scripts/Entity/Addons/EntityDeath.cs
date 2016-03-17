using UnityEngine;

/// <summary>
/// Allows an entity to die. Sends an event when the health <= 0
/// </summary>
[RequireComponent(typeof(EntityHealth))]
public class EntityDeath : EntityAddon
{
	private EntityHealth health;

	private bool dead;

	protected override void Awake()
	{
		base.Awake();

		health = GetComponent<EntityHealth>();
	}

	protected void OnEnable()
	{
		Entity.Events.AddListener<WeaponDamageEvent>(OnDamageReceived);
	}

	protected void OnDisable()
	{
		Entity.Events.RemoveListener<WeaponDamageEvent>(OnDamageReceived);
	}

	private void OnDamageReceived(WeaponDamageEvent evt)
	{
		if(!dead && health.CurrentHealth <= 0)
		{
			dead = true;
			
			EntityDiedEvent diedEvent = new EntityDiedEvent(evt.Damage, Entity);

			GlobalEvents.Invoke(diedEvent);
			Entity.Events.Invoke(diedEvent);

			// Mark the entity as dead
			Entity.AddTag("Dead");
		}
	}

#if UNITY_EDITOR
	[ContextMenu("Kill")]
	private void Kill()
	{
		HitInfo hitInfo = new HitInfo(Entity, Entity, Vector3.zero, Vector3.zero, "Untagged");
		DamageInfo damageInfo = new DamageInfo(hitInfo, health.CurrentHealth);

		GlobalEvents.Invoke(new WeaponDamageEvent(null, damageInfo));
	}
#endif
}