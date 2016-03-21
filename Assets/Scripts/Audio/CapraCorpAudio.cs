using UnityEngine;
using System.Collections;

public class CapraCorpAudio : MonoBehaviour
{
	[SerializeField] private new AudioAssetGroup audio;
	[SerializeField] private Transform[] speakers;

	[SerializeField] private float interval;

	[SerializeField] private float min;
	[SerializeField] private float max;

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

	protected void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;

		foreach(Transform speaker in speakers)
		{
			Gizmos.DrawWireSphere(speaker.position, min);
			Gizmos.DrawWireSphere(speaker.position, max);
		}
	}

	private IEnumerator Play()
	{
		yield return new WaitForSeconds(2f);

		while(true)
		{
			AudioAsset target = audio.GetRandom();

			foreach(Transform speaker in speakers)
			{
				AudioChannel audioChannel = audioManager.PlayAt(target, speaker.position, true);
				audioChannel.AudioSource.minDistance = min;
				audioChannel.AudioSource.maxDistance = max;
			}

			yield return new WaitForSeconds(interval);
		}
	}
}