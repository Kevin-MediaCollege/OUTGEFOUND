using UnityEngine;
using System.Collections;

public abstract class WeaponComponent : MonoBehaviour
{
	protected Weapon weapon;

	protected void Awake()
	{
		weapon = GetComponent<Weapon>() ?? GetComponentInParent<Weapon>();
	}

	protected void OnEnable()
	{
		weapon.onFireEvent += OnFire;
	}

	protected void OnDisable()
	{
		weapon.onFireEvent -= OnFire;
	}

	protected abstract void OnFire(HitInfo hitInfo);
}