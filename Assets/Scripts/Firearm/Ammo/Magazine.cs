using UnityEngine;

public class Magazine : MonoBehaviour
{
	public int Remaining { set; get; }

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

	protected void Awake()
	{
		Remaining = capacity;
	}
}