using UnityEngine;
using System.Collections;

public class MenuCameraPath : MonoBehaviour 
{
	public MenuCameraPosition start;
	public MenuCameraPosition end;

	public Vector3 a;
	public Vector3 b;

	void OnDrawGizmosSelected()
	{
		if(start == null || end == null)
		{
			return;
		}

		Vector3 s = start.transform.position, e = end.transform.position;

		Gizmos.color = new Color(1f, 1f, 1f, 1f);
		Gizmos.DrawLine(s, s + a);
		Gizmos.DrawLine(e, e + b);

		Curve curve = new Curve(s, s + a, e + b, e);
		float lenght = curve.lenght;

		CurveData ccd = curve.getDataAt(0f);
		CurveData ncd;

		Gizmos.color = new Color(1f, 0.3f, 0.3f, 1f);
		for(int i = 0; i < 20; i++)
		{
			ncd = curve.getDataAt((i / 20f) * lenght);
			Gizmos.DrawLine(ccd.pos, ncd.pos);
			ccd = ncd;
		}

		Gizmos.DrawLine(ccd.pos, e);
	}

	public Curve getCurve(int _curveResolution)
	{
		return new Curve(start.transform.position, start.transform.position + a, end.transform.position + b, end.transform.position, _curveResolution);
	}
}
