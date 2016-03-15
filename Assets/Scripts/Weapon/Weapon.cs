using UnityEngine;
using System;
using System.Collections.Generic;

[AddComponentMenu("Weapon/Weapon")]
public class Weapon : MonoBehaviour
{
	public Entity Wielder { private set; get; }

	private HashSet<WeaponComponent> components = new HashSet<WeaponComponent>();

	[SerializeField] protected WeaponAim aim;
	[SerializeField] protected LayerMask layers;

	[SerializeField] protected float damage;

	protected bool firing;
	
	protected virtual void OnEnable()
	{
		if(Wielder == null)
		{
			Wielder = GetComponentInParent<Entity>();
		}

		Wielder.Events.AddListener<StartFireEvent>(OnStartFireEvent);
		Wielder.Events.AddListener<StopFireEvent>(OnStopFireEvent);
	}

	protected virtual void OnDisable()
	{
		Wielder.Events.RemoveListener<StartFireEvent>(OnStartFireEvent);
		Wielder.Events.RemoveListener<StopFireEvent>(OnStopFireEvent);
	}

	protected virtual void FixedUpdate()
	{
		if(firing)
		{
			foreach(WeaponComponent component in components)
			{
				component.TryFire();
			}

			if(CanFire())
			{
				Vector3 direction = GetAimDirection();
				HitInfo hit = GetHitInfo(direction);

				foreach(WeaponComponent component in components)
				{
					component.OnFire(hit);
				}

				GlobalEvents.Invoke(new WeaponFireEvent(this, hit));

				if(hit.target != null)
				{
					if(hit.target.GetComponentInChildren<Damagable>() != null)
					{
						DamageInfo damage = new DamageInfo(hit, GetDamage(hit));
						WeaponDamageEvent damageEvent = new WeaponDamageEvent(this, damage);

						foreach(WeaponComponent component in components)
						{
							component.OnDamage(damage);
						}

						GlobalEvents.Invoke(damageEvent);
						hit.target.Events.Invoke(damageEvent);
					}
				}
			}
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

	public void StartFire()
	{
		if(!firing && CanFire())
		{
			firing = true;

			foreach(WeaponComponent component in components)
			{
				component.OnStartFire();
			}
		}
	}

	public void StopFire(bool @override = false)
	{
		if(firing)
		{
			if(!@override)
			{
				foreach(WeaponComponent component in components)
				{
					if(!component.CanStopFire())
					{
						return;
					}
				}
			}

			firing = false;

			foreach(WeaponComponent component in components)
			{
				component.OnStopFire();
			}
		}
	}

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

	protected virtual HitInfo GetHitInfo(Vector3 direction)
	{
		throw new NotImplementedException();
	}

	protected virtual Vector3 GetBaseAimDirection()
	{
		throw new NotImplementedException();
	}

	protected virtual Vector3 GetAimDirection()
	{
		Vector3 direction = GetBaseAimDirection();

		foreach(WeaponComponent component in components)
		{
			direction = component.GetAimDirection(direction);
		}

		return direction.normalized;
	}

	private float GetDamage(HitInfo hit)
	{
		float result = damage;

		foreach(WeaponComponent component in components)
		{
			result = component.GetDamage(hit, result);
		}

		return result;
	}

	private void OnStartFireEvent(StartFireEvent evt)
	{
		StartFire();
	}

	private void OnStopFireEvent(StopFireEvent evt)
	{
		StopFire();
	}
}