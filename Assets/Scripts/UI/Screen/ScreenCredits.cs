using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScreenCredits : ScreenBase 
{
	public override string Name
	{
		get
		{
			return "ScreenCredits";
		}
	}

	public CanvasGroup group;
	public RectTransform rect;

	public Touchable buttonBack;
	public Touchable buttonEaster;

	public Animator door_credits;
	public Animator door_easter;

	void Awake()
	{
		buttonBack.OnPointerDownEvent += OnBackButton;
		buttonEaster.OnPointerDownEvent += OnEasterButton;
	}

	void OnBackButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		door_credits.SetBool("Open", false);
		MenuCamera.instance.prepare("Credits", "Menu");
		ScreenManager.Instance.SetScreen("ScreenMainMenu");
	}

	void OnEasterButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		door_easter.SetBool("Open", true);
		MenuCamera.instance.prepare("Credits", "Easter");
		ScreenManager.Instance.SetScreen("ScreenEasteregg");
	}

	public override void OnScreenEnter()
	{
		rect.anchoredPosition3D = new Vector3(-620f, 0f, 0f);
		HOTweenHelper.Fade(group, 0f, 1f, 0.2f, 0f);
		HOTweenHelper.Position(rect, new Vector3(0f, 0f, 0f), 0.2f, 0f, Ease.OutCubic);
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
		HOTweenHelper.Position(rect, new Vector3(-620f, 0f, 0f), 0.2f, 0f, Ease.OutCubic);
		yield return new WaitForSeconds(0.1f);
		yield return MenuCamera.instance.flyFromTo("", "");
	}
}
