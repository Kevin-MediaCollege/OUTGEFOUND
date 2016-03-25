using UnityEngine;
using System.Collections;

public class SettingLoaderMouseSensitivity : SettingLoader
{
	[SerializeField] private GameCamera @object;
	[SerializeField] private Vector2 @default;

	protected void Awake()
	{
		float x = PlayerPrefs.GetFloat(key + "_X", @default.x);
		float y = PlayerPrefs.GetFloat(key + "_Y", @default.y);

		@object.Sensitivity = new Vector2(x, y);
	}
}