using UnityEngine;
using System.Collections;

public class SettingLoaderADS : SettingLoader
{
	[SerializeField] private PlayerInputController @object;
	[SerializeField] private bool @default;

	protected void Awake()
	{
		bool setting = PlayerPrefs.GetInt(key, @default ? 1 : 0) == 1 ? true : false;
		@object.ToggleADS = setting;
	}
}