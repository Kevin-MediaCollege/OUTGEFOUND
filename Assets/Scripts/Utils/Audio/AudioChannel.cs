using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioChannel : MonoBehaviour
{
	public AudioAsset AudioAsset { private set; get; }

	public AudioSource AudioSource { private set; get; }

	public float Volume
	{
		set	{ AudioSource.volume = Mathf.Clamp01(value); }
		get { return AudioSource.volume; }
	}

	public float Pitch
	{
		set	{ AudioSource.pitch = Mathf.Clamp01(value); }
		get { return AudioSource.pitch; }
	}

	public float PanStereo
	{
		set	{ AudioSource.panStereo = Mathf.Clamp01(value); }
		get { return AudioSource.panStereo; }
	}

	public float SpatialBlend
	{
		set	{ AudioSource.spatialBlend = Mathf.Clamp01(value); }
		get { return AudioSource.spatialBlend; }
	}

	public float ReverbZoneMix
	{
		set	{ AudioSource.reverbZoneMix = Mathf.Clamp(value, 0, 1.1f); }
		get { return AudioSource.reverbZoneMix; }
	}

	public bool Mute
	{
		set { AudioSource.mute = value; }
		get { return AudioSource.mute; }
	}

	public bool Loop
	{
		set { AudioSource.loop = value; }
		get { return AudioSource.loop; }
	}

	public bool IsPlaying { get { return AudioSource.isPlaying; } }

	public bool IsClaimed { set; get; }

	protected void Awake()
	{
		AudioSource = GetComponent<AudioSource>();
		AudioSource.playOnAwake = false;
		AudioSource.maxDistance = 10;
	}

	public void Play(AudioAsset audioAsset, bool loop = false)
	{
		if(audioAsset.AudioClip == null)
		{
			Debug.LogError("[AudioChannel] " + audioAsset + " has no AudioClip.", audioAsset);
			return;
		}

		AudioSource.clip = audioAsset.AudioClip;
		AudioSource.outputAudioMixerGroup = audioAsset.AudioMixerGroup;

		Volume = audioAsset.Volume;
		Pitch = audioAsset.Pitch;
		PanStereo = audioAsset.StereoPan;
		SpatialBlend = audioAsset.SpatialBlend;
		ReverbZoneMix = audioAsset.ReverbZoneMix;
		Loop = loop;

		AudioAsset = audioAsset;

		AudioSource.Play();
	}

	public void Pause()
	{
		AudioSource.Pause();
	}

	public void UnPause()
	{
		AudioSource.UnPause();
	}

	public void Stop()
	{
		AudioSource.Stop();
	}
}