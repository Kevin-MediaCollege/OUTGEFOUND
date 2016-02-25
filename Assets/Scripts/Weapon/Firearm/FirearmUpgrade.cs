using UnityEngine;
using System;

[Flags]
public enum FireMode
{
	Automatic = 1,
	Burst = 2,
	SemiAutomatic = 4
}

public class FirearmUpgrade : WeaponUpgrade
{
	public FireMode FireModes
	{
		get
		{
			return fireModes;
		}
	}

	public float ReloadSpeed
	{
		get
		{
			return reloadSpeed;
		}
	}

	public float BulletSpread
	{
		get
		{
			return bulletSpread;
		}
	}

	public float MaxRange
	{
		get
		{
			return maxRange;
		}
	}

	public int MagazineCapacity
	{
		get
		{
			return magazineCapacity;
		}
	}

	// FireMode.Burst Configuration
	public int ShotsPerBurst
	{
		get
		{
			return shotsPerBurst;
		}
	}

	[SerializeField] private FireMode fireModes;

	[SerializeField] private float reloadSpeed;
	[SerializeField] private float bulletSpread;
	[SerializeField] private float maxRange;

	[SerializeField] private int magazineCapacity;

	// FireMode.Burst Configuration
	[SerializeField] private int shotsPerBurst;
}
