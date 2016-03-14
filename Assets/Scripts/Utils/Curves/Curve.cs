using UnityEngine;
using System.Collections;

public class Curve
{
	public float lenght;

	private int arrayLenght;
	private CurveArrayData[] array;
	private Vector3 endPos;
	private float endDir;

	public Curve(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, int _resolution = 40)
	{
		arrayLenght = _resolution;
		array = new CurveArrayData[arrayLenght];
		recalculate(_a, _b, _c, _d);
	}

	public void recalculate(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d)
	{
		Vector3 pos = Bezier.get4(_a, _b, _c, _d, 0f);
		Vector3 nextPos;
		Vector3 dir;
		float rotY;
		float dist = 0f;
		lenght = 0f;

		for(int i = 1; i < arrayLenght + 1; i++)
		{
			dir = Bezier.getFirstDerivative4(_a, _b, _c, _d, (i - 1) / ((float)arrayLenght)).normalized;
			rotY = MathHelper.vec3ToRotationY(dir);
			nextPos = Bezier.get4(_a, _b, _c, _d, i / ((float)arrayLenght));
			dist = MathHelper.disVec3(pos, nextPos);

			array[i - 1] = new CurveArrayData(lenght, lenght + dist, rotY, pos, nextPos);
			lenght += dist;
			pos = nextPos;
		}

		endPos = pos;
		endDir = MathHelper.vec3ToRotationY(Bezier.getFirstDerivative4(_a, _b, _c, _d, 1f).normalized);
	}

	public CurveData getDataAt(float _curvePos)
	{
		if(_curvePos <= 0f)
		{
			return array[0].getStart();
		}

		for(int i = 0; i < arrayLenght; i++)
		{
			if(array[i].start <= _curvePos && array[i].end > _curvePos)
			{
				return array[i].getData(_curvePos);
			}
		}

		return new CurveData(endPos, endDir);
	}
}

public class CurveData
{
	public CurveData(Vector3 _pos, float _direction)
	{
		pos = _pos;
		direction = _direction;
	}

	public Vector3 pos;
	public float direction;
}

public class CurveArrayData
{
	public float start;
	public float end;
	public float lenght;
	public float direction;
	public Vector3 posStart;
	public Vector3 posEnd;

	public CurveArrayData(float _start, float _end, float _direction, Vector3 _posStart, Vector3 _posEnd)
	{
		start = _start;
		end = _end;
		lenght = _end - _start;
		direction = _direction;
		posStart = _posStart;
		posEnd = _posEnd;
	}

	public CurveData getData(float _pos)
	{
		_pos = (_pos - start) / lenght;
		return new CurveData((posStart * (1f - _pos)) + (posEnd * _pos), direction);
	}
	
	public CurveData getStart()
	{
		return new CurveData(posStart, direction);
	}
}