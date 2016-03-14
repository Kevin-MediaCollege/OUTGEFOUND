using UnityEngine;
using System.Collections;

public class ScreenEasteregg : ScreenBase 
{
	public override string Name
	{
		get
		{
			return "ScreenEasteregg";
		}
	}

	public CanvasGroup group;

	public Touchable buttonBack;

	public Animator door_easter;

	void Awake()
	{
		buttonBack.OnPointerDownEvent += OnBackButton;
	}

	void OnBackButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		ScreenManager.Instance.SetScreen("ScreenCredits");
	}

	public override void OnScreenEnter()
	{
		HOTweenHelper.Fade(group, 0f, 1f, 0.2f, 0f);
	}

	public override void OnScreenExit()
	{
	}

	public override IEnumerator OnScreenFadeIn()
	{
		yield break;
	}

	public override IEnumerator OnScreenFadeOut()
	{
		HOTweenHelper.Fade(group, 1f, 0f, 0.2f, 0f);
		door_easter.SetBool("Open", false);
		yield return MenuCamera.instance.flyFromTo("Easter", "Credits");
	}
}
