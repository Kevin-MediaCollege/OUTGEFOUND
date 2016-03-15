using UnityEngine;
using System.Collections;

public abstract class WeaponComponent : MonoBehaviour
{
	protected Weapon Weapon { private set; get; }

	protected virtual void Awake()
	{
		if(Weapon == null)
		{
			Weapon = GetComponent<Weapon>() ?? GetComponentInParent<Weapon>();
		}
	}

	protected virtual void OnEnable()
	{
		if(Weapon == null)
		{
			Weapon = GetComponent<Weapon>() ?? GetComponentInParent<Weapon>();
		}

		Weapon.AddComponent(this);
	}

	protected virtual void OnDisable()
	{
		Weapon.RemoveComponent(this);
	}

	public virtual void OnStartFire()
	{
	}

	public virtual void OnStopFire()
	{
	}

	public virtual void TryFire()
	{
	}

	public virtual void OnFire(HitInfo hit)
	{
	}

	public virtual void OnDamage(DamageInfo damage)
	{
	}

	public virtual Vector3 GetAimDirection(Vector3 direction)
	{
		return direction;
	}

	public virtual float GetDamage(HitInfo hit, float damage)
	{
		return damage;
	}

	public virtual bool CanFire()
	{
		return true;
	}

	public virtual bool CanStopFire()
	{
		return true;
	}
}