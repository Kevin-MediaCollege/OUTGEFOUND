using UnityEngine;
using System.Collections;

public class StockPile : MonoBehaviour
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

	protected void Awake()
	{
		current = max + start;
		Reload();
	}
	
	protected void OnEnable()
	{
		magazine.onMagazineEmptyEvent += OnMagazineEmptyEvent;
	}

	protected void OnDisable()
	{
		magazine.onMagazineEmptyEvent -= OnMagazineEmptyEvent;
	}

	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}
	}

	public void Add(int amount)
	{
		current += Mathf.Clamp(amount, 0, max - current);
	}

	public void Reload()
	{
		int amount = magazine.Capacity - magazine.Current;
		amount = Mathf.Min(amount, current);
		
		current -= amount;
		magazine.Put(amount);		
	}

	private void OnMagazineEmptyEvent()
	{
		Reload();
	}
}