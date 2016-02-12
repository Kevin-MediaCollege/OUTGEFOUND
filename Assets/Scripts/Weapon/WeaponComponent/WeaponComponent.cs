using UnityEngine;
using System.Collections;

public abstract class WeaponComponent : MonoBehaviour
{
	protected Weapon weapon;

	protected virtual void Awake()
	{
		weapon = GetComponentInParent<Weapon>();
	}

	protected virtual void OnEnable()
	{
		weapon.AddComponent(this);
	}

	protected virtual void OnDisable()
	{
		weapon.RemoveComponent(this);
	}

	public virtual void Fire(HitInfo info)
	{
	}

	public virtual bool CanFire()
	{
		return true;
	}
}