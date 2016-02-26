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

	protected override void OnEnable()
	{
		base.OnEnable();

		weapon.Entity.Events.AddListener<ReloadWeaponEvent>(OnReloadEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		weapon.Entity.Events.RemoveListener<ReloadWeaponEvent>(OnReloadEvent);
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

		weapon.Entity.Events.Invoke(new ReloadedEvent(weapon));
	}

	private IEnumerator ReloadDelay()
	{
		reloading = true;
		magazine.Deduct(magazine.Current);

		yield return new WaitForSeconds(((FirearmUpgrade)weapon.Upgrade).ReloadSpeed);

		Reload();
		reloading = false;
	}

	private void OnReloadEvent(ReloadWeaponEvent evt)
	{
		if(!reloading && evt.Entity == weapon.Entity)
		{
			StartCoroutine("ReloadDelay");
		}
	}
}