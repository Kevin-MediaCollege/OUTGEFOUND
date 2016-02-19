using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenSplashUnity : ScreenBase 
{
	public CanvasGroup overlay;
	public CanvasGroup logo;

	public new Transform camera;

	//-22.14, 14.85, 28.71
	//26.4359, 364.8974, 0

	//-2.6, 14.85, 26.9
	//26.4359, 347.54, 0

	public override void OnScreenEnter()
	{
		overlay.alpha = 1f;
		logo.alpha = 0f;
	}

	public override IEnumerator OnScreenFadein()
	{
		yield return new WaitForSeconds (1f);

		StartCoroutine (Animation());
	}

	public IEnumerator Animation()
	{
		camera.position = new Vector3(-15.1f, 14.85f, 21.8f);

		HOTweenHelper.TransformPosition (camera, new Vector3(-15.1f, 5.5f, 31.6f), 6f, 0f, Holoville.HOTween.EaseType.EaseOutCubic);

		HOTweenHelper.Fade (overlay, 1f, 0f, 2.5f, 0f);

		yield return new WaitForSeconds (2.5f);

		HOTweenHelper.Fade (logo, 0f, 1f, 2f, 0f);

		yield return new WaitForSeconds (4f);

		HOTweenHelper.Fade (overlay, 0f, 1f, 2.5f, 0f);

		yield return new WaitForSeconds (1.5f);

		HOTweenHelper.Fade (logo, 1f, 0f, 1f, 0f);

		yield return new WaitForSeconds (1.5f);

		SceneManager.LoadSceneAsync("Menu");
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadSceneAsync("Menu");
		}
	}

	public override IEnumerator OnScreenFadeout()
	{
		yield break;
	}

	public override void OnScreenExit()
	{
	}

	public override string getScreenName()
	{
		return "ScreenSplashUnity";
	}
}
