using UnityEngine;

public class SettingSetterFloat : SettingSetter<float>
{
	public override void Set(float value)
	{
		PlayerPrefs.SetFloat(key, value);
	}
}