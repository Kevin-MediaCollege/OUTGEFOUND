using UnityEngine;
using System.Collections;

public class Gunshot : BaseWeaponModifier
{
	[SerializeField] private AudioAsset gunshotAudio;
	[SerializeField] private Vector2 pitchRange;

	public override void OnFire(ref DamageInfo info)
	{
		AudioChannel channel = AudioManager.PlayAt(gunshotAudio, transform.position);
		channel.Pitch = Random.Range(pitchRange.x, pitchRange.y);
	}
}