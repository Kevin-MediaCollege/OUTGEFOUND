using UnityEngine;
using System.Collections;

public class RoomAudio : MonoBehaviour
{
	[SerializeField] private new AudioAsset audio;

	[SerializeField] private float min;
	[SerializeField] private float max;

	protected void Awake()
	{
		AudioChannel audioChannel = Dependency.Get<AudioManager>().PlayAt(audio, transform.position, true);
		audioChannel.IsClaimed = true;
		audioChannel.AudioSource.minDistance = min;
		audioChannel.AudioSource.maxDistance = max;
	}

	protected void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, min);
		Gizmos.DrawWireSphere(transform.position, max);
	}
}