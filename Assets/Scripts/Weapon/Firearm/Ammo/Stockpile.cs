using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Ammo/Stockpile")]
public class Stockpile : WeaponComponent
{
	public int Remaining { private set; get; }

	public int Max
	{
		get
		{
			return max;
		}
	}

	public Magazine Magazine
	{
		get
		{
			return magazine;
		}
	}

	public bool Full
	{
		get
		{
			return Remaining == Max;
		}
	}

	public bool Empty
	{
		get
		{
			return Remaining <= 0;
		}
	}

	[SerializeField] private Magazine magazine;

	[SerializeField] private int max;
	[SerializeField] private bool unlimited;
	
	protected void Start()
	{
		Remaining = max;
		FillMagazine();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		Weapon.Wielder.Events.AddListener<RefillAmmoEvent>(OnRefillAmmoEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		Weapon.Wielder.Events.RemoveListener<RefillAmmoEvent>(OnRefillAmmoEvent);
	}

	public void Add(int amount)
	{
		Remaining += amount;
	}

	public void Fill()
	{
		Remaining = Max;
	}

	public void FillMagazine()
	{
		int amount = Mathf.Min(magazine.Capacity, Remaining);
		magazine.Add(amount);

		if(!unlimited)
		{
			Remaining -= amount;
		}
	}

	private void OnRefillAmmoEvent(RefillAmmoEvent evt)
	{
		Fill();
	}
}