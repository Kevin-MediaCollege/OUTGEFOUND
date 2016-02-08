using UnityEngine;
using System.Collections;

public abstract class BaseDamageModifier : MonoBehaviour, IDamageModifier
{
	private Entity entity;
	private Weapon weapon;

	protected virtual void Awake()
	{
		entity = GetComponentInParent<Entity>();
		weapon = entity.GetComponentInChildren<Weapon>();
	}

	protected virtual void OnEnable()
	{
		weapon.AddDamageModifier(this);
	}

	protected virtual void OnDisable()
	{
		weapon.RemoveDamageModifier(this);
	}

	public abstract HitInfo Modify(HitInfo hitInfo);
}