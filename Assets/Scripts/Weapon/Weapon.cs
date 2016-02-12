using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IEntityInjector
{
	public delegate void OnFire(HitInfo hitInfo);
	public event OnFire onFireEvent = delegate { };

	public Entity Wielder { private set; get; }

	private HashSet<WeaponComponent> components = new HashSet<WeaponComponent>();

	private bool firing;

	protected void FixedUpdate()
	{
		if(firing)
		{
			if(!CanFire())
			{
				return;
			}

			HitInfo hitInfo = GetHitInfo();
			foreach(WeaponComponent component in components)
			{
				component.Fire(hitInfo);
			}

			onFireEvent(hitInfo);
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
			if(CanFire())
			{
				firing = true;
			}			
		}
	}

	public virtual void StopFire(bool force = false)
	{
		if(firing)
		{
			firing = false;
		}
	}

	public void AddComponent(WeaponComponent component)
	{
		components.Add(component);
	}

	public void RemoveComponent(WeaponComponent component)
	{
		components.Remove(component);
	}

	protected abstract HitInfo GetHitInfo();

	public bool CanFire()
	{
		foreach(WeaponComponent component in components)
		{
			if(!component.CanFire())
			{
				return false;
			}
		}

		return true;
	}

	
}