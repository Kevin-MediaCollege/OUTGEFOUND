using DG.Tweening;

public static class AudioUtils
{
	public static void FadeTo(AudioChannel channel, float time, float volume, Ease ease = Ease.Linear)
	{
		DOTween.To(() => channel.AudioSource.volume, a => channel.AudioSource.volume = a, volume, time).SetEase(ease);
	}

	public static void FadeIn(AudioChannel channel, float time, Ease ease = Ease.Linear)
	{
		FadeTo(channel, time, 1, ease);
	}

	public static void FadeOut(AudioChannel channel, float time, Ease ease = Ease.Linear)
	{
		FadeTo(channel, time, 0, ease);
	}
}