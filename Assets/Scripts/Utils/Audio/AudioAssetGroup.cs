using UnityEngine;
using System.Collections.Generic;

public class AudioAssetGroup : ScriptableObject
{
	public IEnumerable<AudioAsset> AudioAssets { get { return audioAssets; } }

	[SerializeField] private AudioAsset[] audioAssets;

	public AudioAsset GetRandom()
	{
		return audioAssets[Random.Range(0, audioAssets.Length)];
	}
}