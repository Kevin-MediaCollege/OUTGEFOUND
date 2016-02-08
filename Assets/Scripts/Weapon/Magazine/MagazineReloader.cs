using UnityEngine;
using System.Collections;

public class MagazineReloader : MonoBehaviour
{
	[SerializeField] private Magazine magazine;

	[SerializeField] private int max;

	private int amount;

	protected void Awake()
	{
		amount = max;
	}

	protected void OnEnable()
	{
		magazine.onMagazineEmptyEvent += OnMagazineEmptyEvent;
	}

	protected void OnDisable()
	{
		magazine.onMagazineEmptyEvent -= OnMagazineEmptyEvent;
	}

	public void Add(int amount)
	{
		this.amount += amount;
	}

	public void Remove(int amount)
	{
		this.amount -= amount;
	}

	private void OnMagazineEmptyEvent()
	{
		int amount = Mathf.Min(this.amount, magazine.Capacity);

		Remove(amount);
		magazine.Put(amount);

		Debug.Log("Reloaded: " + amount);
	}
}