using UnityEngine;
using System.Collections;

public class ScreenStart : ScreenBase 
{
	public CanvasGroup group;
	public CanvasGroup overlay;

	public override void OnScreenEnter()
	{
		overlay.alpha = 1f;
	}

	public override IEnumerator OnScreenFadein()
	{
		yield return new WaitForSeconds (0.1f);

		HOTweenHelper.Fade (overlay, 1f, 0f, 0.5f, 0f);

		yield return new WaitForSeconds (0.5f);
	}

	void Update()
	{
		if(Input.anyKeyDown)
		{
			ScreenManager.Instance.setScreen ("ScreenMainMenu");
		}
	}

	public override IEnumerator OnScreenFadeout()
	{
		Debug.Log ("fadeout");
		yield break;
	}

	public override void OnScreenExit()
	{
		Debug.Log ("exit");
	}

	public override string getScreenName()
	{
		return "ScreenStart";
	}
}
