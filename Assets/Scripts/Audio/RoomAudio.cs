using UnityEngine;
using System.Collections;

public class RoomAudio : MonoBehaviour
{
	private enum Mode
	{
		TwoD,
		ThreeD,
	}

	[SerializeField] private new AudioAsset audio;
	[SerializeField] private Mode mode;

	[SerializeField] private float min;
	[SerializeField] private float max;

	protected void Awake()
	{
		AudioChannel audioChannel = Dependency.Get<AudioManager>().PlayAt(audio, transform.position, true);
		audioChannel.IsClaimed = true;

		if(mode == Mode.ThreeD)
		{
			audioChannel.SpatialBlend = 1;
			audioChannel.AudioSource.minDistance = min;
			audioChannel.AudioSource.maxDistance = max;
		}
		else
		{
			audioChannel.SpatialBlend = 0;
		}
	}

	protected void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, min);
		Gizmos.DrawWireSphere(transform.position, max);
	}
}