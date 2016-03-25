using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsLoader : MonoBehaviour
{
	[SerializeField] private AudioMixer audioMixer;

	protected void Awake()
	{
		ApplySettings();
	}
	
	protected void OnEnable()
	{
		GlobalEvents.AddListener<ReloadSettingsEvent>(OnReloadSettingsEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<ReloadSettingsEvent>(OnReloadSettingsEvent);
	}

	private void ApplySettings()
	{
		audioMixer.SetFloat("Master_Volume", AudioSettings.MasterVolume);
		audioMixer.SetFloat("Music_Volume", AudioSettings.MusicVolume);
		audioMixer.SetFloat("SFX_Volume", AudioSettings.SFXVolume);
		audioMixer.SetFloat("Ambient_Volume", AudioSettings.AmbientVolume);
		audioMixer.SetFloat("UI_Volume", AudioSettings.UIVolume);
		audioMixer.SetFloat("Speech_Volume", AudioSettings.SpeechVolume);
	}

	private void OnReloadSettingsEvent(ReloadSettingsEvent evt)
	{
		ApplySettings();
	}
}
