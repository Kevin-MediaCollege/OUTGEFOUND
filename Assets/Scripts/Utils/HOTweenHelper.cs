using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core.Easing;
using Holoville.HOTween.Plugins.Core;

public class HOTweenHelper 
{
	public static void Fade(CanvasGroup _group, float _alphaStart, float _alphaTarget, float _time, float _delay = 0f)
	{
		_group.alpha = _alphaStart;
		HOTween.To( _group, _time, new TweenParms().Prop( "alpha", _alphaTarget ).Ease( EaseType.Linear ).Delay( _delay ));
	}

	public static void Size(RectTransform _rect, Vector2 _targetSize, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseOutElastic)
	{
		HOTween.To (_rect, _time, new TweenParms ().Prop ("sizeDelta", new PlugVector2 (_targetSize)).Ease (_easeType));
	}

	public static void Position(RectTransform _rect, Vector3 _targetPosition, float _time, float _delay, Holoville.HOTween.EaseType _easeType = Holoville.HOTween.EaseType.EaseOutElastic)
	{
		HOTween.To (_rect, _time, new TweenParms ().Prop ("anchoredPosition3D", new PlugVector3 (_targetPosition)).Ease (_easeType));
	}
}