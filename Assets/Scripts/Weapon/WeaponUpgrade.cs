using UnityEngine;
using System.Collections;
using System;

[Serializable]
public struct DamageMultipliers
{
	public float Head;
	public float Body;
	public float Limbs;
}

public class WeaponUpgrade : ScriptableObject
{
	public GameObject Model
	{
		get
		{
			return model;
		}
	}

	public DamageMultipliers DamageMultipliers
	{
		get
		{
			return damageMultipliers;
		}
	}

	public float BaseDamage
	{
		get
		{
			return baseDamage;
		}
	}

	public float RateOfFire
	{
		get
		{
			return rateOfFire;
		}
	}

	[SerializeField] private GameObject model;
	[SerializeField] private DamageMultipliers damageMultipliers;

	[SerializeField] private float baseDamage;
	[SerializeField] private float rateOfFire;
}