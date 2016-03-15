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
		if(helper != null)
		{
			Object.Destroy(helper.gameObject);
		}
	}

	public void AddDecal(Vector3 point, Vector3 normal, string tag)
	{
		if(helper == null)
		{
			Debug.LogWarning("No decal manager found");
			return;
		}

		helper.AddDecal(point, normal, tag);
	}
}
