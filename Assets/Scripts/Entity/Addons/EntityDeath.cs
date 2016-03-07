using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityHealth))]
public class EntityDeath : BaseEntityAddon
{
	private EntityHealth health;

	private bool dead;

	protected override void Awake()
	{
		base.Awake();

		health = GetComponent<EntityHealth>();
	}

	protected void LateUpdate()
	{
		if(!dead && health.CurrentHealth <= 0)
		{
			dead = true;

			EntityDiedEvent evt = new EntityDiedEvent(Entity);

			GlobalEvents.Invoke(evt);
			Entity.Events.Invoke(evt);

			// Temp
			Destroy(Entity.gameObject);
		}
	}

#if UNITY_EDITOR
	[ContextMenu("Kill")]
	private void Kill()
	{
		HitInfo hitInfo = new HitInfo(Entity, Entity);
		DamageInfo damageInfo = new DamageInfo(hitInfo, health.CurrentHealth);

		GlobalEvents.Invoke(new DamageEvent(damageInfo));
	}
#endif
}