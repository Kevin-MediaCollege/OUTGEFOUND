using System.Collections;
using UnityEngine;

public class AudioSettings : ScriptableObjectSingleton<AudioSettings>
{
	public static float MasterVolume { set; get; }

	public static float MusicVolume { set; get; }

	public static float SFXVolume { set; get; }

	public static float AmbientVolume { set; get; }

	public static float UIVolume { set; get; }

	public static float SpeechVolume { set; get; }

	public static void ApplyModifiedChanges()
	{
		PlayerPrefs.SetFloat("SETTING_AUDIO_MASTER_VOLUME", MasterVolume);
		PlayerPrefs.SetFloat("SETTING_AUDIO_MUSIC_VOLUME", MusicVolume);
		PlayerPrefs.SetFloat("SETTING_AUDIO_SFX_VOLUME", SFXVolume);
		PlayerPrefs.SetFloat("SETTING_AUDIO_AMBIENT_VOLUME", AmbientVolume);
		PlayerPrefs.SetFloat("SETTING_AUDIO_UI_VOLUME", UIVolume);
		PlayerPrefs.SetFloat("SETTING_AUDIO_SPEECH_VOLUME", SpeechVolume);
	}

	public static void Load()
	{
		MasterVolume = PlayerPrefs.GetFloat("SETTING_AUDIO_MASTER_VOLUME", Instance.defaultMasterVolume);
		MusicVolume = PlayerPrefs.GetFloat("SETTING_AUDIO_MUSIC_VOLUME", Instance.defaultMusicVolume);
		SFXVolume = PlayerPrefs.GetFloat("SETTING_AUDIO_SFX_VOLUME", Instance.defaultSfxVolume);
		AmbientVolume = PlayerPrefs.GetFloat("SETTING_AUDIO_AMBIENT_VOLUME", Instance.defaultAmbientVolume);
		UIVolume = PlayerPrefs.GetFloat("SETTING_AUDIO_UI_VOLUME", Instance.defaultUiVolume);
		SpeechVolume = PlayerPrefs.GetFloat("SETTING_AUDIO_SPEECH_VOLUME", Instance.defaultSpeechVolume);
	}

	public static void Cancel()
	{
		Load();
	}

	[SerializeField] private float defaultMasterVolume;
	[SerializeField] private float defaultMusicVolume;
	[SerializeField] private float defaultSfxVolume;
	[SerializeField] private float defaultAmbientVolume;
	[SerializeField] private float defaultUiVolume;
	[SerializeField] private float defaultSpeechVolume;

	protected void OnEnable()
	{
		Load();
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Settings/Audio")]
	private static void Create()
	{
		CreateAsset("Create AudioSettings", "AudioSettings", "Create AudioSettings");
	}
#endif
}