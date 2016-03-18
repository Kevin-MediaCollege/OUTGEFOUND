using UnityEngine;
using System.Collections;

public class CapraCorpAudio : MonoBehaviour
{
	[SerializeField] private new AudioAssetGroup audio;
	[SerializeField] private float interval;

	private AudioManager audioManager;

	protected void Awake()
	{
		audioManager = Dependency.Get<AudioManager>();
	}

	protected void OnEnable()
	{
		StartCoroutine("Play");
	}

	protected void OnDisable()
	{
		StopCoroutine("Play");
	}

	private IEnumerator Play()
	{
		while(true)
		{
			audioManager.PlayRandom(audio);

			yield return new WaitForSeconds(interval);
		}
	}
}