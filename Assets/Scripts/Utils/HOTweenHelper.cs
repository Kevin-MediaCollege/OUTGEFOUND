using DG.Tweening;
using UnityEngine;

public static class HOTweenHelper 
{
	public static Tweener Fade(CanvasGroup group, float alphaStart, float alphaTarget, float time, float delay = 0f)
	{
		group.alpha = alphaStart;
		return DOTween.To(() => alphaStart, a => group.alpha = a, alphaTarget, time).SetEase(Ease.Linear).SetDelay(delay);
	}

	public static Tweener Size(RectTransform rect, Vector2 targetSize, float time, float delay, Ease ease = Ease.OutElastic)
	{
		return DOTween.To(() => rect.sizeDelta, a => rect.sizeDelta = a, targetSize, time).SetEase(ease).SetDelay(delay);
	}

	public static Tweener Scale(RectTransform rect, Vector3 targetScale, float time, float delay, Ease ease = Ease.OutElastic)
	{
		return rect.DOScale(targetScale, time).SetEase(ease).SetDelay(delay);
	}

	public static Tweener Position(RectTransform rect, Vector3 targetPosition, float time, float delay, Ease ease = Ease.OutElastic)
	{
		return DOTween.To(() => rect.anchoredPosition3D, a => rect.anchoredPosition3D = a, targetPosition, time).SetEase(ease).SetDelay(delay);
	}

	public static Tweener TransformPosition(Transform trans, Vector3 targetPosition, float time, float delay, Ease ease = Ease.OutElastic)
	{
		return trans.DOMove(targetPosition, time).SetEase(ease).SetDelay(delay);
	}

	public static Tweener TransformLocalPosition(Transform trans, Vector3 targetPosition, float time, float delay, Ease ease = Ease.OutElastic)
	{
		return DOTween.To(() => trans.localPosition, a => trans.localPosition = a, targetPosition, time).SetEase(ease).SetDelay(delay);
	}

	public static Tweener Rotate(Transform trans, Quaternion targetQuaternion, float time, float delay, Ease ease = Ease.InOutCubic)
	{
		return trans.DORotateQuaternion(targetQuaternion, time).SetEase(ease).SetDelay(delay);
	}
}