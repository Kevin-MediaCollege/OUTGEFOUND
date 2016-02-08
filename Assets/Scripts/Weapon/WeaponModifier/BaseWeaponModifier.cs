using UnityEngine;
using System.Collections;

public abstract class BaseWeaponModifier : MonoBehaviour, IWeaponModifier
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
		weapon.AddWeaponModifier(this);
	}

	protected virtual void OnDisable()
	{
		weapon.RemoveWeaponModifier(this);
	}

	public abstract void OnFire();

	public abstract bool CanFire();
}