using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core.Easing;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;

public class HOTweenHelper 
{
	public static Tweener Fade(CanvasGroup _group, float _alphaStart, float _alphaTarget, float _time, float _delay = 0f)
	{
		_group.alpha = _alphaStart;
		return HOTween.To( _group, _time, new TweenParms().Prop( "alpha", _alphaTarget ).Ease( EaseType.Linear ).Delay( _delay ));
	}

	public static Tweener Size(RectTransform _rect, Vector2 _targetSize, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseOutElastic)
	{
		return HOTween.To (_rect, _time, new TweenParms ().Prop ("sizeDelta", new PlugVector2 (_targetSize)).Ease (_easeType));
	}

	public static Tweener Scale(RectTransform _rect, Vector3 _targetScale, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseOutElastic)
	{
		return HOTween.To (_rect, _time, new TweenParms ().Prop ("localScale", new PlugVector3 (_targetScale)).Ease (_easeType));
	}

	public static Tweener Position(RectTransform _rect, Vector3 _targetPosition, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseOutElastic)
	{
		return HOTween.To (_rect, _time, new TweenParms ().Prop ("anchoredPosition3D", new PlugVector3 (_targetPosition)).Ease (_easeType));
	}

	public static Tweener TransformPosition(Transform _trans, Vector3 _targetPosition, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseOutElastic)
	{
		return HOTween.To (_trans, _time, new TweenParms ().Prop ("position", new PlugVector3 (_targetPosition)).Ease (_easeType));
	}

	public static Tweener Rotate(Transform _trans, Quaternion _targetQuaternion, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseInOutCubic)
	{
		return HOTween.To (_trans, _time, new TweenParms ().Prop ("rotation", new PlugQuaternion (_targetQuaternion)).Ease (_easeType));
	}
}