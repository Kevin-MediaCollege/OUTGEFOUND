using UnityEngine;
using UnityEngine.Audio;

public class SettingSetterAudio : SettingSetter<float>
{
	[SerializeField] protected AudioMixer @object;

	public override void Set(float value)
	{
	}

	public void SetMaster(float value)
	{
		Set("Master_Volume", value);
	}

	public void SetMusic(float value)
	{
		Set("Music_Volume", value);
	}

	public void SetSFX(float value)
	{
		Set("SFX_Volume", value);
	}

	private void Set(string name, float value)
	{
		@object.SetFloat(name, value);
	}
}