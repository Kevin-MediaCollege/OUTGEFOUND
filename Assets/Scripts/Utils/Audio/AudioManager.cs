using UnityEngine;
using System.Collections.Generic;

public class AudioManager : IDependency
{
	public int NUM_CHANNELS = 128;
	
	private HashSet<AudioChannel> channels;
	private Transform helperObject;

	public AudioManager()
	{
		channels = new HashSet<AudioChannel>();
		helperObject = new GameObject("AudioManager").transform;
		helperObject.gameObject.AddComponent<KeepInScene>();


		for(int i = 0; i < NUM_CHANNELS; i++)
		{
			CreateChannel();
		}
	}

	public AudioChannel Play(AudioAsset audioAsset, bool loop = false)
	{
		if(audioAsset == null)
		{
			return null;
		}

		AudioChannel channel = GetAvailableChannel();

		if(channel != null)
		{
			channel.Play(audioAsset, loop);
			return channel;
		}

		Debug.LogWarning("[AudioManager] No available audio channels for audio asset: " + audioAsset);
		return null;
	}

	public AudioChannel PlayAt(AudioAsset audioAsset, Vector3 point, bool loop = false)
	{
		AudioChannel channel = Play(audioAsset, loop);

		if(channel != null)
		{
			channel.transform.position = point;
		}

		return channel;
	}
		
	public AudioChannel PlayRandom(AudioAssetGroup audioAssetGroup, bool loop = false)
	{
		if(audioAssetGroup == null)
		{
			return null;
		}

		List<AudioAsset> audioAssets = new List<AudioAsset>(audioAssetGroup.AudioAssets);
		AudioAsset target = audioAssets[Random.Range(0, audioAssets.Count)];

		return Play(target, loop);
	}

	public void StopAll(AudioAssetType type, bool includeClaimedChannels = false)
	{
		foreach(AudioChannel channel in channels)
		{
			if(includeClaimedChannels && channel.IsClaimed)
			{
				continue;
			}
				
			if(channel.IsPlaying && channel.AudioAsset.Type == type)
			{
				channel.Stop();
			}
		}
	}

	public void StopAll(bool includeClaimedChannels = false)
	{
		foreach(AudioChannel channel in channels)
		{
			if(includeClaimedChannels && channel.IsClaimed)
			{
				continue;
			}

			channel.Stop();
		}
	}

	private AudioChannel GetAvailableChannel()
	{
		foreach(AudioChannel channel in channels)
		{
			if(!channel.IsPlaying && !channel.IsClaimed)
			{
				return channel;
			}
		}
		return null;
	}

	private AudioChannel CreateChannel()
	{
		if(channels.Count < NUM_CHANNELS)
		{
			GameObject obj = new GameObject("Audio Channel " + channels.Count);
			AudioChannel channel = obj.AddComponent<AudioChannel>();

			obj.transform.SetParent(helperObject);
			channels.Add(channel);

			return channel;
		}

		return null;
	}
}