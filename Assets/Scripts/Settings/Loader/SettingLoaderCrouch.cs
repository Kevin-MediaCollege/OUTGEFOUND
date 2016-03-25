using UnityEngine;
using System.Collections;

public class SettingLoaderCrouch : SettingLoader
{
	[SerializeField] private PlayerInputController @object;
	[SerializeField] private bool @default;

	protected void Awake()
	{
		bool setting = PlayerPrefs.GetInt(key, @default ? 1 : 0) == 1 ? true : false;
		@object.ToggleCrouch = setting;
	}
}