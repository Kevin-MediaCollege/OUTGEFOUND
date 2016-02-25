using UnityEngine;

public class Magazine : WeaponModifier
{
	public delegate void OnMagazineEmpty();
	public event OnMagazineEmpty onMagazineEmptyEvent = delegate { };
	
	public int Current { private set; get; }

	public override void OnFire(HitInfo hitInfo)
	{
		Current--;

		if(Current <= 0)
		{
			onMagazineEmptyEvent();
		}
	}

	public void Put(int amount)
	{
		Current += amount;
	}

	public void Deduct(int amount)
	{
		Current -= amount;
	}
}