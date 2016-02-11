using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour, IEntityInjector
{
	public delegate void OnFireEvent(DamageInfo info);
	public event OnFireEvent onFireEvent = delegate { };

	public Entity Wielder { private set; get; }

	[SerializeField] protected int baseDamage;

	private HashSet<IWeaponModifier> weaponModifiers;

	private bool firing;

	protected void Awake()
	{
		if(weaponModifiers == null)
		{
			weaponModifiers = new HashSet<IWeaponModifier>();
		}
	}

	protected void FixedUpdate()
	{
		if(firing)
		{
			if(!CanFire())
			{
				return;
			}

			DamageInfo info;
			GetshotInfo(out info);

			// Apply all modifiers to the shot
			foreach(IWeaponModifier modifier in weaponModifiers)
			{
				modifier.OnFire(ref info);
			}

			onFireEvent(info);
			OnFire();

			// Let the target know it's been damaged
			if(info.Hit)
			{
				info.Target.Damagable.Damage(info);
			}
		}
	}

	public void RegisterEntity(Entity entity)
	{
		Wielder = entity;
	}

	public virtual void StartFire()
	{
		if(!firing)
		{
			firing = true;
		}
	}

	public virtual void StopFire(bool force = false)
	{
		if(firing)
		{
			firing = false;
		}
	}

	protected virtual void OnFire()
	{
	}

	public bool CanFire()
	{
		foreach(IWeaponModifier modifier in weaponModifiers)
		{
			if(!modifier.CanFire())
			{
				return false;
			}
		}

		return true;
	}

	#region Modifier Handlers
	public void AddWeaponModifier(IWeaponModifier modifier)
	{
		if(weaponModifiers == null)
		{
			weaponModifiers = new HashSet<IWeaponModifier>();
		}

		weaponModifiers.Add(modifier);
	}

	public void RemoveWeaponModifier(IWeaponModifier modifier)
	{
		weaponModifiers.Remove(modifier);
	}
	#endregion

	protected abstract bool GetshotInfo(out DamageInfo info);
}