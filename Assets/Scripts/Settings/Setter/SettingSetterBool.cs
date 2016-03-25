using UnityEngine;

public class SettingSetterBool : SettingSetter<bool>
{
	public override void Set(bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}
}