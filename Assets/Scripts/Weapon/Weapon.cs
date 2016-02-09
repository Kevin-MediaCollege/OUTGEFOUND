using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour, IEntityInjector
{
	public delegate void OnFire(ShotInfo shotInfo);
	public event OnFire onFireEvent = delegate { };

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

	protected void Update()
	{
		if(firing)
		{
			if(!CanFire())
			{
				return;
			}

			ShotInfo shotInfo;
			GetshotInfo(out shotInfo);

			// Apply all modifiers to the shot
			foreach(IWeaponModifier modifier in weaponModifiers)
			{
				modifier.OnFire(ref shotInfo);
			}

			onFireEvent(shotInfo);

			// Let the target know it's been damaged
			if(shotInfo.HitDamagable)
			{
				shotInfo.Target.Damage(shotInfo);
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

	public virtual void StopFire()
	{
		if(firing)
		{
			firing = false;
		}
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

	protected abstract bool GetshotInfo(out ShotInfo shotInfo);
}