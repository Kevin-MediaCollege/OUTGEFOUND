using UnityEngine;

public class SettingSetterInt : SettingSetter<int>
{
	public override void Set(int value)
	{
		PlayerPrefs.SetInt(key, value);
	}
}