using UnityEngine;
using System.Collections;

public class FirearmAudio : WeaponComponent
{
	[SerializeField] private AudioAsset gunShot;
	[SerializeField] private Vector2 pitchRange;

	protected override void OnFire(HitInfo hitInfo)
	{
		AudioChannel audioChannel = AudioManager.PlayAt(gunShot, ((Firearm)weapon).BarrelPosition);

		if(audioChannel != null)
		{
			audioChannel.Pitch = Random.Range(pitchRange.x, pitchRange.y);
		}
	}
}