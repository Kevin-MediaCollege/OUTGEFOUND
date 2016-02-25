using UnityEngine;
using System.Collections;

public abstract class WeaponComponent : MonoBehaviour
{
	protected Weapon weapon;

	protected virtual void Awake()
	{
		weapon = GetComponent<Weapon>() ?? GetComponentInParent<Weapon>();
	}

	protected virtual void OnEnable()
	{
		GlobalEvents.AddListener<WeaponFireEvent>(OnWeaponFireEvent);
	}

	protected virtual void OnDisable()
	{
		GlobalEvents.RemoveListener<WeaponFireEvent>(OnWeaponFireEvent);
	}

	protected abstract void OnFire(HitInfo hitInfo);

	private void OnWeaponFireEvent(WeaponFireEvent evt)
	{
		if(evt.Weapon == weapon)
		{
			OnFire(evt.HitInfo);
		}
	}
}