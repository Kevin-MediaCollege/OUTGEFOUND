using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
	protected void Start()
	{
		SceneLoader sceneLoader = Dependency.Get<SceneLoader>();

#if !UNITY_EDITOR
		sceneLoader.Load("SplashScreen");
#else
		if(SceneManager.sceneCount == 1)
		{
			sceneLoader.Load("SplashScreen");
		}
		else
		{
			Dependency.Get<GameDependency>().Start();
		}
#endif
	}
}
