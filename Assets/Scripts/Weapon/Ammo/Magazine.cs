using UnityEngine;
using System.Collections;
using System;

public class Magazine : MonoBehaviour
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

	public bool Fire(Firearm firearm)
	{
		Current--;

		if(Current <= 0)
		{
			onMagazineEmptyEvent();
			return false;
		}

		return true;
	}

	public void Put(int amount)
	{
		Current += amount;
	}
}