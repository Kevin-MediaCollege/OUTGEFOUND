using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour
{
	public Entity Entity { private set; get; }

	private HashSet<IWeaponModifier> weaponModifiers = new HashSet<IWeaponModifier>();
	private HashSet<IDamageModifier> damageModifiers = new HashSet<IDamageModifier>();

	protected virtual void Awake()
	{
		Entity = GetComponentInParent<Entity>();
	}

	public void TryFire()
	{
		if(CanFire())
		{
			Fire();

			foreach(IWeaponModifier modifier in weaponModifiers)
			{
				modifier.OnFire();
			}
		}
	}

	public bool CanFire()
	{
		bool result = true;

		foreach(IWeaponModifier modifier in weaponModifiers)
		{
			result &= modifier.CanFire();
		}

		return result;
	}

	public void AddWeaponModifier(IWeaponModifier modifier)
	{
		weaponModifiers.Add(modifier);
	}

	public void AddDamageModifier(IDamageModifier modifier)
	{
		damageModifiers.Add(modifier);
	}

	public void RemoveWeaponModifier(IWeaponModifier modifier)
	{
		weaponModifiers.Remove(modifier);
	}

	public void RemoveDamageModifier(IDamageModifier modifier)
	{
		damageModifiers.Remove(modifier);
	}

	protected abstract void Fire();

	protected HitInfo ApplyDamageModifiers(HitInfo hitInfo)
	{
		HitInfo result = hitInfo;

		foreach(IDamageModifier modifier in damageModifiers)
		{
			result = modifier.Modify(hitInfo);
		}

		return result;
	}
}