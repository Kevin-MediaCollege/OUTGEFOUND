using UnityEngine;
using System.Collections;

public class RotationHelper
{
	public static float rotationToPoint(Vector3 _pos, Vector3 _target)
	{
		return Mathf.Atan2 ((_target.x - _pos.x), (_target.z - _pos.z)) * 180f / Mathf.PI;
	}

	public static float fixRotation(float _a, float _b)
	{
		if(_b > _a + 180f)
		{
			_b -= 360f;
		}
		else if(_b < _a - 180f)
		{
			_b += 360f;
		}

		return _b;
	}
}
