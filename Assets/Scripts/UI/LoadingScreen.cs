using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
	public CanvasGroup group;

	void Awake () 
	{
		StartCoroutine ("fadeIn");
	}

	public IEnumerator fadeIn()
	{
		HOTweenHelper.Fade (group, 1f, 0f, 1f, 0f);

		yield return new WaitForSeconds (1.5f);

		/*
		 *  EN UNCOMMENCT DIT OOK EFFE KROL
		 
		AsyncOperation loader = SceneManager.LoadSceneAsync ("");

		while(!loader.isDone)
		{
			yield return null;
		}

		HOTweenHelper.Fade (group, 0f, 1f, 0.7f, 0f);

		yield return new WaitForSeconds (0.7f);
		*/

		//HE KEVIN DOE HIER DE GAME AAN
	}
}
