using UnityEngine;

/// <summary>
/// Dependency for managing bullet impact decals
/// </summary>
public class DecalManager : IGameDependency
{
	private DecalManagerHelper helper;

	public void Start()
	{
		GameObject helperObject = new GameObject("Decal Manager Helper");
		helper = helperObject.AddComponent<DecalManagerHelper>();
	}

	public void Stop()
	{
		if(helper.gameObject != null)
		{
			Object.Destroy(helper.gameObject);
		}
	}
}
