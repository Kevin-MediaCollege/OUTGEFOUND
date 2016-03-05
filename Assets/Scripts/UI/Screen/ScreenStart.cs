using UnityEngine;
using System.Collections;
using System;

public class ScreenStart : ScreenBase 
{
	public override string Name
	{
		get
		{
			return "ScreenStart";
		}
	}

	[SerializeField] private CanvasGroup group;
	[SerializeField] private CanvasGroup overlay;

	protected void Update()
	{
		if(Input.anyKeyDown)
		{
			ScreenManager.Instance.SetScreen("ScreenMainMenu");
		}
	}

	public override void OnScreenEnter()
	{
		overlay.alpha = 1f;
	}

	public override IEnumerator OnScreenFadeIn()
	{
		yield return new WaitForSeconds (0.1f);

		HOTweenHelper.Fade(overlay, 1f, 0f, 0.5f, 0f);

		yield return new WaitForSeconds (0.5f);
	}
	public override IEnumerator OnScreenFadeOut()
	{
		yield break;
	}

	public override void OnScreenExit()
	{
	}
}
