using UnityEngine;
using System.Collections;

public class FirearmAudio : WeaponComponent
{
	[SerializeField] private AudioAsset gunShot;
	[SerializeField] private Vector2 pitchRange;

	protected override void OnFire(HitInfo hitInfo)
	{
		// TODO: Play sound at barrel's position instead of weapon position
		AudioChannel audioChannel = AudioManager.PlayAt(gunShot, weapon.transform.position);

		if(audioChannel != null)
		{
			audioChannel.Pitch = Random.Range(pitchRange.x, pitchRange.y);
		}
	}
}