using UnityEngine;
using System.Collections.Generic;

public class AudioAssetGroup : ScriptableObject
{
	public IEnumerable<AudioAsset> AudioAssets { get { return audioAssets; } }

	[SerializeField] private AudioAsset[] audioAssets;
}