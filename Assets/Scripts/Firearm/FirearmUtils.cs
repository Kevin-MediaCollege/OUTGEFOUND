using System.Collections;
using UnityEngine;

public static class FirearmUtils
{
	private static float lastGunshotTime;

	public static AudioChannel PlayGunshot(AudioManager audioManager, AudioAsset audioAsset, Vector3 position)
	{
		if(Time.time - lastGunshotTime > 0.1f)
		{
			lastGunshotTime = Time.time;
			return audioManager.PlayAt(audioAsset, position);
		}

		return null;
	}
}