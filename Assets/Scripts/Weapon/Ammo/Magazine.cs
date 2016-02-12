using UnityEngine;
using System.Collections;
using System;

public class Magazine : WeaponComponent
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

	public override void Fire(HitInfo info)
	{
		Current--;
			
		if(Current <= 0)
		{
			Debug.Log("[Magazine] Empty");

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