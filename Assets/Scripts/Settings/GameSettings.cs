using System;
using System.Collections;
using UnityEngine;

public class GameSettings : ScriptableObjectSingleton<GameSettings>
{
	public static float MouseSensitivityX { set; get; }

	public static float MouseSensitivityY { set; get; }

	public static bool ToggleADS { set; get; }

	public static bool ToggleCrouch { set; get; }
	
	public static void ApplyModifiedChanges()
	{
		PlayerPrefs.SetFloat("SETTING_GAME_MOUSE_SENSITIVITY_X", MouseSensitivityX);
		PlayerPrefs.SetFloat("SETTING_GAME_MOUSE_SENSITIVITY_Y", MouseSensitivityY);
		PlayerPrefs.SetInt("SETTING_GAME_MOUSE_TOGGLE_ADS", Convert.ToInt32(ToggleADS));
		PlayerPrefs.SetInt("SETTING_GAME_MOUSE_TOGGLE_CROUCH", Convert.ToInt32(ToggleCrouch));
	}

	public static void Load()
	{
		MouseSensitivityX = PlayerPrefs.GetFloat("SETTING_GAME_MOUSE_SENSITIVITY_X", Instance.defaultMouseSensitivityX);
		MouseSensitivityY = PlayerPrefs.GetFloat("SETTING_GAME_MOUSE_SENSITIVITY_Y", Instance.defaultMouseSensitivityY);
		ToggleADS = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GAME_MOUSE_TOGGLE_ADS", Convert.ToInt32(Instance.defaultToggleADS)));
		ToggleCrouch = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GAME_MOUSE_TOGGLE_CROUCH", Convert.ToInt32(Instance.defaultToggleCrouch)));
	}

	public static void Cancel()
	{
		Load();
	}

	[SerializeField] private float defaultMouseSensitivityX;
	[SerializeField] private float defaultMouseSensitivityY;
	[SerializeField] private bool defaultToggleADS;
	[SerializeField] private bool defaultToggleCrouch;

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Settings/Game")]
	private static void Create()
	{
		CreateAsset("Create GameSettings", "GameSettings", "Create GameSettings");
	}
#endif
}