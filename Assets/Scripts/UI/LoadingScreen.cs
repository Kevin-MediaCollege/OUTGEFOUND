using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] private CanvasGroup fadeInGroup;

	[SerializeField] private new Camera camera;

	protected void Start() 
	{
		StartCoroutine("FadeIn");
	}

	private IEnumerator FadeIn()
	{
		HOTweenHelper.Fade(fadeInGroup, 1f, 0f, 1f, 0f);

		yield return new WaitForSeconds(1.5f);

		SceneManager.LoadSceneAsync("Level_1");
	}
}
