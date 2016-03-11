using UnityEngine;
using System.Collections;

public class ScreenStart : ScreenBase, ICommunicant
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

	private IEventDispatcher eventDispatcher;

	protected void Update()
	{
		if(Input.anyKeyDown)
		{
			eventDispatcher.Invoke(new StateStartGameEvent());
			//ScreenManager.Instance.SetScreen("ScreenMainMenu");
		}
	}

	public void RegisterEventDispatcher(IEventDispatcher eventDispatcher)
	{
		this.eventDispatcher = eventDispatcher;
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
