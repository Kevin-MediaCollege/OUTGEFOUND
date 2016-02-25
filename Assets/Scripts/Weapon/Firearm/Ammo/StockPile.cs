using UnityEngine;
using System.Collections;

public class StockPile : WeaponModifier
{
	public int Max
	{
		get
		{
			return max;
		}
	}

	public int Current
	{
		get
		{
			return current;
		}
	}

	[SerializeField] private int start;
	[SerializeField] private int max;

	[SerializeField] private Magazine magazine;

	private int current;
	private bool reloading;

	protected void Start()
	{
		current = max + start;
		Reload();
	}

	protected void Update()
	{
		if(!reloading && Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine("ReloadDelay");
		}
	}

	public void Add(int amount)
	{
		current += Mathf.Clamp(amount, 0, max - current);
	}

	public void Reload()
	{
		int amount = ((FirearmUpgrade)weapon.Upgrade).MagazineCapacity - magazine.Current;
		amount = Mathf.Min(amount, current);
		
		current -= amount;
		magazine.Put(amount);		
	}

	private IEnumerator ReloadDelay()
	{
		magazine.Deduct(magazine.Current);

		yield return new WaitForSeconds(((FirearmUpgrade)weapon.Upgrade).ReloadSpeed);

		Reload();
		reloading = false;
	}
}