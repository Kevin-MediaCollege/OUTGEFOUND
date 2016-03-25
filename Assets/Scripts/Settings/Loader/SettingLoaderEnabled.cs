using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SettingLoaderEnabled : SettingLoader
{
	[SerializeField] private MonoBehaviour @object;
	[SerializeField] private bool @default;

	protected void Awake()
	{
		bool enabled = PlayerPrefs.GetInt(key, @default ? 1 : 0) == 1 ? true : false;
		@object.enabled = enabled;
	}
}