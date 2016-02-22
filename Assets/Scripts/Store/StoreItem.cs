using UnityEngine;
using System.Collections;

public abstract class StoreItem : MonoBehaviour
{
	public string Name
	{
		get
		{
			return itemName;
		}
	}

	public string Description
	{
		get
		{
			return description;
		}
	}

	public int Cost
	{
		get
		{
			return cost;
		}
	}

	[SerializeField] protected string itemName;
	[SerializeField] protected string description;
	[SerializeField] protected int cost;

	public abstract void Purchase();
}