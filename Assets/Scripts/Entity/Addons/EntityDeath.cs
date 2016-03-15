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
		health.onDamageReceivedEvent += OnDamageReceived;
	}

	protected void OnDisable()
	{
		health.onDamageReceivedEvent -= OnDamageReceived;
	}

	private void OnDamageReceived(DamageInfo damageInfo)
	{
		if(!dead && health.CurrentHealth <= 0)
		{
			dead = true;
			
			EntityDiedEvent evt = new EntityDiedEvent(damageInfo, Entity);

			GlobalEvents.Invoke(evt);
			Entity.Events.Invoke(evt);

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