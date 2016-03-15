using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Ammo/Magazine")]
public class Magazine : WeaponComponent
{
	public int Remaining { private set; get; }

	public int Capacity
	{
		get
		{
			return capacity;
		}
	}

	public bool Full
	{
		get
		{
			return Remaining == Capacity;
		}
	}

	public bool Empty
	{
		get
		{
			return Remaining <= 0;
		}
	}

	[SerializeField] private int capacity;

	public override void OnFire(HitInfo hit)
	{
		Remaining--;

		if(Remaining <= 0)
		{
			Weapon.Wielder.Events.Invoke(new MagazineEmptyEvent());
			Weapon.StopFire(true);
		}
	}

	public override bool CanFire()
	{
		return Remaining > 0;
	}

	public void Add(int amount)
	{
		Remaining += amount;
	}

	public void Remove(int amount)
	{
		Remaining -= amount;
	}
}