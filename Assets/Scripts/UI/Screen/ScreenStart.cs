using UnityEngine;
using System.Collections;

public class ScreenStart : ScreenBase 
{
	public CanvasGroup group;

	public override void OnScreenEnter()
	{
		Debug.Log ("enter");
	}

	public override IEnumerator OnScreenFadein()
	{
		Debug.Log ("fadein");
		yield break;
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
