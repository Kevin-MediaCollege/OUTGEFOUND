using UnityEngine;

public class Magazine : WeaponModifier
{
	public int Current { private set; get; }

	public override void OnFire(HitInfo hitInfo)
	{
		Current--;

		if(Current <= 0)
		{
			weapon.Entity.Events.Invoke(new MagazineEmptyEvent(weapon));
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