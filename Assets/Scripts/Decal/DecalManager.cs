using UnityEngine;

/// <summary>
/// Dependency for managing bullet impact decals
/// </summary>
public class DecalManager : IDependency
{
	private DecalManagerHelper helper;

	public void Create()
	{
		GameObject helperObject = new GameObject("Decal Manager Helper");
		helper = helperObject.AddComponent<DecalManagerHelper>();
	}

	public void Destroy()
	{
		Object.Destroy(helper.gameObject);
	}
}
