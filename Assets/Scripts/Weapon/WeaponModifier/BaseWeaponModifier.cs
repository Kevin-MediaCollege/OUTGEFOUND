using UnityEngine;
using System.Collections;

public abstract class BaseWeaponModifier : MonoBehaviour, IWeaponModifier
{
	protected Weapon weapon;

	protected virtual void Awake()
	{
		weapon = GetComponentInParent<Weapon>();
	}

	protected virtual void OnEnable()
	{
		weapon.AddWeaponModifier(this);
	}

	protected virtual void OnDisable()
	{
		weapon.RemoveWeaponModifier(this);
	}

	protected void Reset()
	{
		weapon = GetComponentInParent<Weapon>();
	}

	public virtual void OnFire(ref DamageInfo info)
	{
	}

	public virtual bool CanFire()
	{
		return true;
	}
}