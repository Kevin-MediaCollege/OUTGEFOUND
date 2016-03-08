using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the loading of scenes
/// </summary>
public class SceneLoader : IDependency
{
	public bool Loading { private set; get; }

	public float Progress { private set; get; }

	private CoroutineRunner coroutineRunner;

	public SceneLoader()
	{
		coroutineRunner = Dependency.Get<CoroutineRunner>();
	}

	public void Load(string name, bool additive = false)
	{
		if(!Loading)
		{
			coroutineRunner.StartCoroutine(LoadScene(name, additive));

			Loading = true;
		}
	}

	private IEnumerator LoadScene(string name, bool additive)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

		while(!async.isDone)
		{
			Progress = async.progress;
			yield return null;
		}

		Loading = false;
	}
}