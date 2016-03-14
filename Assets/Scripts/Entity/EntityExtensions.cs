using UnityEngine;

/// <summary>
/// Useful extension methods for Entity
/// </summary>
public static class EntityExtensions
{
	public static Magazine GetMagazine(this Entity entity)
	{
		return entity.GetComponentInChildren<Magazine>();
	}

	public static AmmoStockPile GetStockPile(this Entity entity)
	{
		return entity.GetComponentInChildren<AmmoStockPile>();
	}

	public static Transform GetEyes(this Entity entity)
	{
		return entity.transform.Find("Eyes");
	}
}