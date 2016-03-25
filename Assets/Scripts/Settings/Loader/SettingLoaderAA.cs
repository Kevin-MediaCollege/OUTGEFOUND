using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SettingLoaderAA : SettingLoader
{
	[SerializeField] private Antialiasing @object;
	[SerializeField] private AAMode @default;

	protected void Awake()
	{
		int setting = PlayerPrefs.GetInt(key, (int)@default);
		@object.mode = (AAMode)setting;
	}
}