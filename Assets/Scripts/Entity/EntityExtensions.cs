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

	public static Stockpile GetStockPile(this Entity entity)
	{
		return entity.GetComponentInChildren<Stockpile>();
	}

	public static Transform GetEyes(this Entity entity)
	{
		return entity.transform.Find("Eyes");
	}

	public static EntityHealth GetHealth(this Entity entity)
	{
		return entity.GetComponentInChildren<EntityHealth>();
	}
}