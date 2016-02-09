using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;

	public int NumChannels
	{
		set
		{
			numChannels = value;
		}
		get
		{
			return numChannels;
		}
	}

	[SerializeField] private int numChannels;

	private HashSet<AudioChannel> channels;

	protected void Awake()
	{
		channels = new HashSet<AudioChannel>();
		instance = this;

		for(int i = 0; i < NumChannels; i++)
		{
			CreateChannel();
		}
	}

	public static AudioChannel Play(AudioAsset audioAsset, bool loop = false)
	{
		if(audioAsset == null)
		{
			return null;
		}

		AudioChannel channel = instance.GetAvailableChannel();

		if(channel != null)
		{
			channel.Play(audioAsset, loop);
			return channel;
		}

		Debug.LogWarning("[AudioManager] No available audio channels for audio asset: " + audioAsset);
		return null;
	}
		
	public static AudioChannel PlayRandom(AudioAssetGroup audioAssetGroup, bool loop = false)
	{
		if(audioAssetGroup == null)
		{
			return null;
		}

		List<AudioAsset> audioAssets = new List<AudioAsset>(audioAssetGroup.AudioAssets);
		AudioAsset target = audioAssets[Random.Range(0, audioAssets.Count)];

		return Play(target, loop);
	}

	public static void StopAll(AudioAssetType type, bool includeClaimedChannels = false)
	{
		foreach(AudioChannel channel in instance.channels)
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

	public static void StopAll(bool includeClaimedChannels = false)
	{
		foreach(AudioChannel channel in instance.channels)
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
		if(channels.Count < NumChannels)
		{
			GameObject obj = new GameObject("Audio Channel " + channels.Count);
			AudioChannel channel = obj.AddComponent<AudioChannel>();

			obj.transform.SetParent(transform);
			channels.Add(channel);

			return channel;
		}

		return null;
	}
}