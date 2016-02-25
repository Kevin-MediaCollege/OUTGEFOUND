using UnityEngine;
using System.Collections;

public abstract class WeaponModifier : MonoBehaviour
{
	protected Weapon weapon;

	protected virtual void Awake()
	{
		weapon = GetComponent<Weapon>() ?? GetComponentInParent<Weapon>();
	}

	protected void OnEnable()
	{
		weapon.AddModifier(this);

		GlobalEvents.AddListener<WeaponFireEvent>(OnFireEvent);
	}

	protected void OnDisable()
	{
		weapon.RemoveModifier(this);

		GlobalEvents.RemoveListener<WeaponFireEvent>(OnFireEvent);
	}

	public virtual bool CanFire()
	{
		return true;
	}

	public virtual void OnFire(HitInfo hitInfo)
	{
	}

	private void OnFireEvent(WeaponFireEvent evt)
	{
		if(evt.Weapon == weapon)
		{
			OnFire(evt.HitInfo);
		}
	}
}