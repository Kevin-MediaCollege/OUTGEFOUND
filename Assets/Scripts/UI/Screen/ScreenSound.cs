using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScreenSound : ScreenBase 
{
	public override string Name
	{
		get
		{
			return "ScreenSound";
		}
	}

	public CanvasGroup group;
	public RectTransform rect;

	public Touchable buttonBack;

	public ComponentSlider slider_master;
	public ComponentSlider slider_music;
	public ComponentSlider slider_ui;
	public ComponentSlider slider_sfx;
	public ComponentSlider slider_ambient;
	public ComponentSlider slider_speech;

	void Awake()
	{
		buttonBack.OnPointerDownEvent += OnBackButton;
	}

	void OnBackButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		MenuCamera.instance.prepare("Audio", "Options");
		ScreenManager.Instance.SetScreen("ScreenOptions");
	}

	public override void OnScreenEnter()
	{
		//load settings:
		slider_master.init(50f);
		slider_music.init(50f);
		slider_ui.init(50f);
		slider_sfx.init(50f);
		slider_ambient.init(50f);
		slider_speech.init(50f);

		rect.anchoredPosition3D = new Vector3(-1920f, 0f, 0f);
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
		HOTweenHelper.Position(rect, new Vector3(-1920f, 0f, 0f), 0.2f, 0f, Ease.OutCubic);
		yield return new WaitForSeconds(0.1f);
		yield return MenuCamera.instance.flyFromTo("", "");
	}
}
