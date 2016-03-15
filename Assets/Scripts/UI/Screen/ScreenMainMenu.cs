using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.Serialization;

public class ScreenMainMenu : ScreenBase
{
	public override string Name
	{
		get
		{
			return "ScreenMainMenu";
		}
	}

	public CanvasGroup group;
	public RectTransform rect;

	public Touchable buttonLevel;
	public Touchable buttonOptions;
	public Touchable buttonCredits;

	public Animator door_credits;
	public Animator door_options;
	public Animator door_play;

	public CanvasGroup overlay;

	void Awake()
	{
		buttonLevel.OnPointerDownEvent += onButtonLevel;
		buttonOptions.OnPointerDownEvent += onButtonOptions;
		buttonCredits.OnPointerDownEvent += onButtonCredits;
		overlay.gameObject.SetActive (false);
	}

	void Update()
	{
	}

	void onButtonLevel (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		buttonLevel.Interactable = false;
		buttonOptions.Interactable = false;
		buttonCredits.Interactable = false;
		StartCoroutine("startLevel");
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

	public override void OnScreenEnter()
	{
		rect.anchoredPosition3D = new Vector3(-620f, 0f, 0f);
		HOTweenHelper.Fade(group, 0f, 1f, 0.2f, 0f);
		HOTweenHelper.Position(rect, new Vector3(0f, 0f, 0f), 0.2f, 0f, Holoville.HOTween.EaseType.EaseOutCubic);
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
		HOTweenHelper.Position(rect, new Vector3(-620f, 0f, 0f), 0.2f, 0f, Holoville.HOTween.EaseType.EaseOutCubic);
		yield return new WaitForSeconds(0.1f);
		yield return MenuCamera.instance.flyFromTo("", "");
	}

	public IEnumerator startLevel()
	{
		HOTweenHelper.Fade(group, 1f, 0f, 0.2f, 0f);
		HOTweenHelper.Position(rect, new Vector3(-620f, 0f, 0f), 0.2f, 0f, Holoville.HOTween.EaseType.EaseOutCubic);
		yield return new WaitForSeconds(0.1f);
		StartCoroutine("openDoorDelayed");
		yield return MenuCamera.instance.flyFromTo("Menu", "Play", 4f);
		SceneManager.LoadScene ("Loading Screen");
	}

	public IEnumerator openDoorDelayed()
	{
		yield return new WaitForSeconds(2f);
		door_play.SetBool("Open", true);
		yield return new WaitForSeconds(1f);
		overlay.gameObject.SetActive (true);
		HOTweenHelper.Fade (overlay, 0f, 1f, 0.5f, 0f);
	}
}
