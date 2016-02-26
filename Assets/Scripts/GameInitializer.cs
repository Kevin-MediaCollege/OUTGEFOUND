using UnityEngine;
using System.Collections;

public class GameInitializer : MonoBehaviour
{
	protected void Awake()
	{
		Dependency.Get<Currency>().Create();
	}

	protected void OnDestroy()
	{
		Dependency.Get<Currency>().Destroy();
	}
}