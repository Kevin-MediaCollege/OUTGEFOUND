using UnityEngine;
using UnityEngine.Audio;

public enum AudioAssetType
{
	SFX,
	MUSIC,
	VOICE_OVER
}

public class AudioAsset : ScriptableObject
{
	public AudioClip AudioClip { get { return audioClip; } }
	public AudioMixerGroup AudioMixerGroup { get { return audioMixerGroup; } }
	public AudioAssetType Type { get { return type;} }

	public float Volume { get { return volume; } }
	public float Pitch { get { return pitch; } }
	public float StereoPan { get { return stereoPan; } }
	public float SpatialBlend { get { return spatialBlend; } }
	public float ReverbZoneMix { get { return reverbZoneMix; } }

	[SerializeField] private AudioClip audioClip;
	[SerializeField] private AudioMixerGroup audioMixerGroup;
	[SerializeField] private AudioAssetType type;

	[SerializeField, Range(0, 1)] private float volume = 1;
	[SerializeField, Range(-3, 3)] private float pitch = 1;
	[SerializeField, Range(0, 1)] private float stereoPan;
	[SerializeField, Range(0, 1)] private float spatialBlend;
	[SerializeField, Range(0, 1.1f)] private float reverbZoneMix = 1;
}