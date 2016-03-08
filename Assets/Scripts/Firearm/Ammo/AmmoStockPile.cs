using UnityEngine;

public class AmmoStockPile : MonoBehaviour
{
	public int Capacity
	{
		get
		{
			return capacity;
		}
	}

	public int Current
	{
		set
		{
			current = Mathf.Clamp(value, 0, Capacity);
		}
		get
		{
			return current;
		}
	}

	[SerializeField] private int start;
	[SerializeField] private int capacity;

	private int current;

	protected void Awake()
	{
		current = start;
	}
}