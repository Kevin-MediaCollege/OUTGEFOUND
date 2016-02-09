using System;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Plugins.Core;

public static class AudioUtils
{
	public static void FadeTo(AudioChannel channel, float time, float volume, EaseType easeType = EaseType.Linear)
	{
		HOTween.To(channel.AudioSource, time, new TweenParms().Prop("volume", new PlugFloat(volume)).Ease(easeType));
	}

	public static void FadeIn(AudioChannel channel, float time, EaseType easeType = EaseType.Linear)
	{
		FadeTo(channel, time, 1, easeType);
	}

	public static void FadeOut(AudioChannel channel, float time, EaseType easeType = EaseType.Linear)
	{
		FadeTo(channel, time, 0, easeType);
	}
}