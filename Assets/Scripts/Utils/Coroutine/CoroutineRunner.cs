using UnityEngine;
using System.Collections;

public class CoroutineRunner : IDependency
{
	private CoroutineRunnerHelper coroutineRunnerHelper;
	private CoroutineRunnerHelper CoroutineRunnerHelper
	{
		get
		{
			if(coroutineRunnerHelper == null)
			{
				GameObject coroutineRunnerObject = new GameObject("CoroutineRunner");
				//Object.DontDestroyOnLoad(coroutineRunnerObject);

				coroutineRunnerHelper = coroutineRunnerObject.AddComponent<CoroutineRunnerHelper>();
			}

			return coroutineRunnerHelper;
		}
	}

	public void Dispose()
	{
		if(coroutineRunnerHelper != null)
		{
			Object.Destroy(coroutineRunnerHelper.gameObject);
		}
	}

	public Coroutine StartCoroutine(IEnumerator routine)
	{
		return CoroutineRunnerHelper.StartCoroutine(routine);
	}

	public void StopCoroutine(IEnumerator routine)
	{
		CoroutineRunnerHelper.StopCoroutine(routine);
	}

	public void StopCoroutine(Coroutine routine)
	{
		CoroutineRunnerHelper.StopCoroutine(routine);
	}
}
