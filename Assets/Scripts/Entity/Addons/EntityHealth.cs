using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IEntityInjector
{
	public float StartingHealth
	{
		get
		{
			return startingHealth;
		}
	}

	public float CurrentHealth { set; get; }

	public Entity Entity { set; get; }

	[SerializeField] private float startingHealth;
	[SerializeField] private GameObject bloodParticlePrefab;

	protected void Awake()
	{
		CurrentHealth = startingHealth;
	}

	protected void OnEnable()
	{
		GlobalEvents.AddListener<DamageEvent>(OnDamageEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<DamageEvent>(OnDamageEvent);
	}

	private void OnDamageEvent(DamageEvent evt)
	{
		if(evt.DamageInfo.Hit.Target == Entity)
		{
			CurrentHealth -= evt.DamageInfo.Damage;

			GameObject bloodParticle = Instantiate(bloodParticlePrefab);
			bloodParticle.transform.position = evt.DamageInfo.Hit.Point;
			bloodParticle.transform.rotation = Quaternion.FromToRotation(transform.forward, evt.DamageInfo.Hit.Normal) * transform.rotation;

			if(CurrentHealth <= 0)
			{
				GlobalEvents.Invoke(new EntityDiedEvent(evt.DamageInfo));
			}
		}
	}

#if UNITY_EDITOR
	[ContextMenu("Heal")]
	private void Heal()
	{
		CurrentHealth = StartingHealth;
	}

	[ContextMenu("Damage")]
	private void Damage()
	{
		HitInfo hitInfo = new HitInfo(Entity, Entity);
		DamageInfo damageInfo = new DamageInfo(hitInfo, 1);

		GlobalEvents.Invoke(new DamageEvent(damageInfo));
	}

	[ContextMenu("Kill")]
	private void Kill()
	{
		HitInfo hitInfo = new HitInfo(Entity, Entity);
		DamageInfo damageInfo = new DamageInfo(hitInfo, CurrentHealth);

		GlobalEvents.Invoke(new DamageEvent(damageInfo));
	}
#endif
}