using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Serialization;

public class ScreenMainMenu : ScreenBase, ICommunicant
{
	public override string Name
	{
		get
		{
			return "ScreenMainMenu";
		}
	}

	public CanvasGroup group;

	public Touchable buttonLevel;
	public Touchable buttonOptions;
	public Touchable buttonCredits;

	public Animator door_credits;
	public Animator door_options;

	private IEventDispatcher eventDispatcher;

	void Awake()
	{
		buttonLevel.OnPointerDownEvent += onButtonLevel;
		buttonOptions.OnPointerDownEvent += onButtonOptions;
		buttonCredits.OnPointerDownEvent += onButtonCredits;
	}

	void Update()
	{
	}

	void onButtonLevel (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		eventDispatcher.Invoke(new StateStartGameEvent());
	}

	void onButtonOptions (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		door_options.SetBool("Open", true);
		MenuCamera.instance.prepare("Menu", "Options");
		ScreenManager.Instance.SetScreen("ScreenOptions");
	}

	void onButtonCredits (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		door_credits.SetBool("Open", true);
		MenuCamera.instance.prepare("Menu", "Credits");
		ScreenManager.Instance.SetScreen("ScreenCredits");
	}

	public void RegisterEventDispatcher(IEventDispatcher eventDispatcher)
	{
		this.eventDispatcher = eventDispatcher;
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

		yield return MenuCamera.instance.flyFromTo("", "");

		yield break;
	}
}
