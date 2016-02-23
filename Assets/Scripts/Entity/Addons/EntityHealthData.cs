using System;
using UnityEngine;

public class EntityHealthData : MonoBehaviour, IEntityInjector
{
	public int StartingHealth
	{
		get
		{
			return startingHealth;
		}
	}

	public int CurrentHealth { set; get; }

	public Entity Entity { set; get; }

	[SerializeField] private int startingHealth;

	protected void OnEnable()
	{
		Dependency.Get<EntityHealth>().Add(Entity, this);
	}

	protected void OnDisable()
	{
		Dependency.Get<EntityHealth>().Remove(Entity);
	}

#if UNITY_EDITOR
	[ContextMenu("Heal")]
	private void Heal()
	{
		Dependency.Get<EntityHealth>().Heal(Entity);
	}

	[ContextMenu("Damage")]
	private void Damage()
	{
		DamageInfo damageInfo = new DamageInfo(Entity, Entity, 1);
		Dependency.Get<EntityHealth>().Damage(damageInfo);
	}

	[ContextMenu("Kill")]
	private void Kill()
	{
		DamageInfo damageInfo = new DamageInfo(Entity, Entity, CurrentHealth);
		Dependency.Get<EntityHealth>().Damage(damageInfo);
	}
#endif
}