using UnityEngine;
using System.Collections;

public class MathHelper
{
	public static float cos(float dir)
	{
		return Mathf.Cos(dir * Mathf.PI / 180f);
	}
	
	public static float sin(float dir)
	{
		return Mathf.Sin(dir * Mathf.PI / 180f);
	}
	
	public static float dis1(float a, float b)
	{
		return Mathf.Sqrt((a - b) * (a - b));
	}
	
	public static float dis2(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}
	
	public static float dis3(float x1, float y1, float z1, float x2, float y2, float z2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
	}
	
	public static float disVec2(Vector2 a, Vector2 b)
	{
		return dis2(a.x, a.y, b.x, b.y);
	}
	
	public static float disVec3(Vector3 a, Vector3 b)
	{
		return dis3(a.x, a.y, a.z, b.x, b.y, b.z);
	}
	
	public static float vec2Length(Vector3 v)
	{
		return dis2(0f, 0f, v.x, v.y);
	}
	
	public static float vec3Length(Vector3 v)
	{
		return dis3(0f, 0f, 0f, v.x, v.y, v.z);
	}
	
	public static float pointToRotation(float x1, float y1, float x2, float y2)
	{
		return Mathf.Atan2((y2 - y1), (x2 - x1)) * 180f / Mathf.PI;
	}

	public static float vec3ToRotationY(Vector3 _a)
	{
		return Mathf.Atan2(_a.z, _a.x) * 180f / Mathf.PI;
	}
}
