using UnityEngine;
using System.Collections;

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
		yield return new WaitForSeconds (1f);

		HOTweenHelper.Fade(overlay, 1f, 0f, 0.5f, 0f);

		yield return new WaitForSeconds (0.5f);
	}
	public override IEnumerator OnScreenFadeOut()
	{
		HOTweenHelper.Fade(group, 1f, 0f, 0.2f, 0f);

		//yield return MenuCamera.instance.flyFromTo("Start", "Menu");

		yield break;
	}

	public override void OnScreenExit()
	{
	}
}
