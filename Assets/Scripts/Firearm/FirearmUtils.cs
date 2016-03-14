using System;
using UnityEngine;

/// <summary>
/// A collection of useful firearm utilities
/// </summary>
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

	public static FireMode GetAvailableFireMode(FireMode available)
	{
		if(HasFireMode(available, FireMode.Automatic))
		{
			return FireMode.Automatic;
		}
		
		if(HasFireMode(available, FireMode.Burst3))
		{
			return FireMode.Burst3;
		}

		if(HasFireMode(available, FireMode.SemiAutomatic))
		{
			return FireMode.SemiAutomatic;
		}

		throw new ArgumentException("No available fire modes");
	}

	public static FireMode GetNextFireMode(FireMode available, FireMode current)
	{
		if(current == FireMode.Automatic)
		{
			return
				HasFireMode(available, FireMode.Burst3) ? FireMode.Burst3 :
				HasFireMode(available, FireMode.SemiAutomatic) ? FireMode.SemiAutomatic :
				FireMode.Automatic;
		}

		if(current == FireMode.Burst3)
		{
			return
				HasFireMode(available, FireMode.SemiAutomatic) ? FireMode.SemiAutomatic :
				HasFireMode(available, FireMode.Automatic) ? FireMode.Automatic :
				FireMode.Burst3;
		}

		if(current == FireMode.SemiAutomatic)
		{
			return
				HasFireMode(available, FireMode.Automatic) ? FireMode.Automatic :
				HasFireMode(available, FireMode.Burst3) ? FireMode.Burst3 :
				FireMode.SemiAutomatic;
		}

		throw new ArgumentException("No available fire modes");
	}

	public static bool HasFireMode(FireMode available, FireMode target)
	{
		return (available & target) == target;
	}
}