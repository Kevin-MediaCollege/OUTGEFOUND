using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour, ICommunicant
{
	[SerializeField] private CanvasGroup fadeInGroup;
	[SerializeField] private CanvasGroup fadeOutGroup;

	[SerializeField] private new Camera camera;

	private IEventDispatcher eventDispatcher;

	protected void Start() 
	{
		StartCoroutine("FadeIn");
	}

	public void RegisterEventDispatcher(IEventDispatcher eventDispatcher)
	{
		this.eventDispatcher = eventDispatcher;
	}

	private IEnumerator FadeIn()
	{
		HOTweenHelper.Fade(fadeInGroup, 1f, 0f, 1f, 0f);

		yield return new WaitForSeconds(1.5f);

		GlobalEvents.AddListener<GameStartedEvent>(OnGameStartedEvent);
		
		eventDispatcher.Invoke(new StateContinueEvent());
	}

	private IEnumerator FadeOut()
	{
		HOTweenHelper.Fade(fadeInGroup, 1f, 0f, 0.7f, 0f);

		yield return new WaitForSeconds(0.7f);

		gameObject.SetActive(false);
		camera.gameObject.SetActive(false);
	}

	private void OnGameStartedEvent(GameStartedEvent evt)
	{
		GlobalEvents.RemoveListener<GameStartedEvent>(OnGameStartedEvent);
		
		StartCoroutine("FadeOut");
	}
}
