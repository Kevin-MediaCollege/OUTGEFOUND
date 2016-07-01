using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScreenOptions : ScreenBase 
{
	public override string Name
	{
		get
		{
			return "ScreenOptions";
		}
	}

	public CanvasGroup group;
	public RectTransform rect;

	public Touchable buttonSound;
	public Touchable buttonGameplay;
	public Touchable buttonDisplay;
	public Touchable buttonGraphics;
	public Touchable buttonBack;

	public Animator door;

	void OnEnable()
	{
		buttonSound.OnPointerDownEvent += OnSoundButton;
		//buttonGameplay.OnPointerDownEvent += OnGameplayButton;
		//buttonDisplay.OnPointerDownEvent += OnDisplayButton;
		//buttonGraphics.OnPointerDownEvent += OnGraphicsButton;
		buttonBack.OnPointerDownEvent += OnBackButton;
	}

	void OnDisable()
	{
		buttonSound.OnPointerDownEvent -= OnSoundButton;
		buttonBack.OnPointerDownEvent -= OnBackButton;
	}

	void OnSoundButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		MenuCamera.instance.prepare ("options", "Audio");
		ScreenManager.Instance.SetScreen("ScreenSound");
	}

	void OnGameplayButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		MenuCamera.instance.prepare ("options", "Controls");
		ScreenManager.Instance.SetScreen("ScreenGameplay");
	}

	void OnDisplayButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		MenuCamera.instance.prepare ("options", "display");
		ScreenManager.Instance.SetScreen("ScreenDisplay");
	}

	void OnGraphicsButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		MenuCamera.instance.prepare ("options", "graphics");
		ScreenManager.Instance.SetScreen("ScreenGraphics");
	}

	void OnBackButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		MenuCamera.instance.prepare ("options", "menu");
		ScreenManager.Instance.SetScreen("ScreenMainMenu");
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
		//HOTweenHelper.Fade(group, 1f, 0f, 0.2f, 0f);
		//door.SetBool("Open", false);
		//yield return MenuCamera.instance.flyFromTo("Options", "Menu");

		HOTweenHelper.Fade(group, 1f, 0f, 0.2f, 0f);
		HOTweenHelper.Position(rect, new Vector3(-620f, 0f, 0f), 0.2f, 0f, Ease.OutCubic);
		yield return new WaitForSeconds(0.1f);
		yield return MenuCamera.instance.flyFromTo("", "");
	}
}
