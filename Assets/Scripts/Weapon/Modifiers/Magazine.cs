using UnityEngine;
using System.Collections;
using System;

public class Magazine : BaseWeaponModifier
{
	public delegate void OnMagazineEmpty();
	public event OnMagazineEmpty onMagazineEmptyEvent = delegate { };

	public int Capacity
	{
		get
		{
			return capacity;
		}
	}

	public int Current { private set; get; }

	[SerializeField] private int capacity;

	protected void Awake()
	{
		Put(capacity);
	}

	// Temp reloading
	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Put(Capacity - Current);
		}
	}

	public override void OnFire(ref DamageInfo info)
	{
		Current--;
			
		if(Current <= 0)
		{
			onMagazineEmptyEvent();
			weapon.StopFire(true);
		}
	}

	public override bool CanFire()
	{
		return Current > 0;
	}

	public void Put(int amount)
	{
		Current += amount;
	}
}