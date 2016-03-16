using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScreenMainMenu : ScreenBase, ICommunicant
{
	public override string Name
	{
		get
		{
			return "ScreenMainMenu";
		}
	}

	[SerializeField] private CanvasGroup group;
	[SerializeField] private RectTransform rect;

	[SerializeField] private Touchable buttonLevel;
	[SerializeField] private Touchable buttonOptions;
	[SerializeField] private Touchable buttonCredits;
	[SerializeField] private Touchable buttonQuit;

	[SerializeField] private Animator door_credits;
	[SerializeField] private Animator door_options;
	[SerializeField] private Animator door_play;

	[SerializeField] private CanvasGroup overlay;

	private IEventDispatcher eventDispatcher;

	protected void Awake()
	{
		buttonLevel.OnPointerDownEvent += OnButtonLevel;
		buttonOptions.OnPointerDownEvent += OnButtonOptions;
		buttonCredits.OnPointerDownEvent += OnButtonCredits;
		buttonQuit.OnPointerDownEvent += OnButtonQuit;

		overlay.gameObject.SetActive(false);
	}

	public void RegisterEventDispatcher(IEventDispatcher eventDispatcher)
	{
		this.eventDispatcher = eventDispatcher;
	}

	void OnButtonLevel (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		buttonLevel.Interactable = false;
		buttonOptions.Interactable = false;
		buttonCredits.Interactable = false;
		StartCoroutine("StartLevel");
	}

	void OnButtonOptions (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		door_options.SetBool("Open", true);
		MenuCamera.instance.prepare("Menu", "Options");
		ScreenManager.Instance.SetScreen("ScreenOptions");
	}

	void OnButtonCredits (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		door_credits.SetBool("Open", true);
		MenuCamera.instance.prepare("Menu", "Credits");
		ScreenManager.Instance.SetScreen("ScreenCredits");
	}

	void OnButtonQuit (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		#if UNITY_EDITOR
		Debug.Log("hype");
		EditorApplication.ExecuteMenuItem("Edit/Play");
		#else
		System.Diagnostics.Process.GetCurrentProcess().Kill();
		#endif
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

	public IEnumerator StartLevel()
	{
		HOTweenHelper.Fade(group, 1f, 0f, 0.2f, 0f);
		HOTweenHelper.Position(rect, new Vector3(-620f, 0f, 0f), 0.2f, 0f, Holoville.HOTween.EaseType.EaseOutCubic);
		yield return new WaitForSeconds(0.1f);
		StartCoroutine("OpenDoorDelayed");
		yield return MenuCamera.instance.flyFromTo("Menu", "Play", 4f);
	}

	public IEnumerator OpenDoorDelayed()
	{
		yield return new WaitForSeconds(2f);
		door_play.SetBool("Open", true);
		yield return new WaitForSeconds(1f);
		overlay.gameObject.SetActive (true);
		HOTweenHelper.Fade (overlay, 0f, 1f, 0.4f, 0f);
		yield return new WaitForSeconds(0.4f);

		eventDispatcher.Invoke(new StateStartGameEvent());
	}
}
