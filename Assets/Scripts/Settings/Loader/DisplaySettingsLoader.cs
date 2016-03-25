using UnityEngine;

public class DisplaySettingsLoader : MonoBehaviour
{
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
		if(Screen.currentResolution.width != DisplaySettings.Resolution.width || Screen.currentResolution.height != DisplaySettings.Resolution.height || Screen.currentResolution.refreshRate != DisplaySettings.Resolution.refreshRate || Screen.fullScreen != !DisplaySettings.Windowed)
		{
			Screen.SetResolution(DisplaySettings.Resolution.width, DisplaySettings.Resolution.height, !DisplaySettings.Windowed, DisplaySettings.Resolution.refreshRate);
		}

		QualitySettings.vSyncCount = DisplaySettings.VSync ? 1 : 0;
		Camera.main.fieldOfView = DisplaySettings.FieldOfView;
	}

	private void OnReloadSettingsEvent(ReloadSettingsEvent evt)
	{
		ApplySettings();
	}
}
