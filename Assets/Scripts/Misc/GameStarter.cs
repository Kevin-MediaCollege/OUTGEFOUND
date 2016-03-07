using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
	protected void Start()
	{
		SceneLoader sceneLoader = Dependency.Get<SceneLoader>();

		if(SceneManager.sceneCount == 1)
		{
			sceneLoader.Load("SplashScreen");
		}
	}
}
