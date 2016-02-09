using UnityEngine;
using System.Collections;

public abstract class BaseWeaponModifier : MonoBehaviour, IWeaponModifier
{
	[SerializeField] protected Weapon weapon;

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

	public virtual void OnFire(ref ShotInfo shotInfo)
	{
	}

	public virtual bool CanFire()
	{
		return true;
	}
}