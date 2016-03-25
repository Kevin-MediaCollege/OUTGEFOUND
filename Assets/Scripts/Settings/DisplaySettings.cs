using System;
using System.Collections;
using UnityEngine;

public class DisplaySettings : ScriptableObjectSingleton<DisplaySettings>
{
	public static Resolution Resolution { set; get; }

	public static bool VSync { set; get; }

	public static bool Windowed { set; get; }

	public static float FieldOfView { set; get; }
	
	public static void ApplyModifiedChanges()
	{
		PlayerPrefs.SetInt("SETTING_DISPLAY_RESOLUTION_WIDTH", Resolution.width);
		PlayerPrefs.SetInt("SETTING_DISPLAY_RESOLUTION_HEIGHT", Resolution.height);
		PlayerPrefs.SetInt("SETTING_DISPLAY_RESOLUTION_REFRESH_RATE", Resolution.refreshRate);
		PlayerPrefs.SetInt("SETTING_DISPLAY_VSYNC", Convert.ToInt32(VSync));
		PlayerPrefs.SetInt("SETTING_DISPLAY_WINDOWED", Convert.ToInt32(Windowed));
		PlayerPrefs.SetFloat("SETTING_DISPLAY_FIELD_OF_VIEW", FieldOfView);

		GlobalEvents.Invoke(new ReloadSettingsEvent());
	}

	public static void Load()
	{
		int resolutionWidth = PlayerPrefs.GetInt("SETTING_DISPLAY_RESOLUTION_WIDTH", Screen.currentResolution.width);
		int resolutionHeight = PlayerPrefs.GetInt("SETTING_DISPLAY_RESOLUTION_HEIGHT", Screen.currentResolution.height);
		int resolutionRefreshRate = PlayerPrefs.GetInt("SETTING_DISPLAY_RESOLUTION_REFRESH_RATE", Screen.currentResolution.refreshRate);

		Resolution[] resolutions = Screen.resolutions;
		foreach(Resolution resolution in resolutions)
		{
			if(resolution.width == resolutionWidth && resolution.height == resolutionHeight && resolution.refreshRate == resolutionRefreshRate)
			{
				Resolution = resolution;
				break;
			}
		}

		VSync = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_DISPLAY_VSYNC", Convert.ToInt32(Instance.defaultVSync)));
		Windowed = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_DISPLAY_WINDOWED", Convert.ToInt32(Instance.defaultWindowed)));
		FieldOfView = PlayerPrefs.GetFloat("SETTING_DISPLAY_FIELD_OF_VIEW", Instance.defaultFieldOfView);
	}

	public static void Cancel()
	{
		Load();
	}
	
	[SerializeField] private bool defaultVSync;
	[SerializeField] private bool defaultWindowed;
	[SerializeField] private float defaultFieldOfView;

	protected void OnEnable()
	{
		Load();
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Settings/Display")]
	private static void Create()
	{
		CreateAsset("Create DisplaySettings", "DisplaySettings", "Create DisplaySettings");
	}
#endif
}