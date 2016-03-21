using UnityEngine;
using System.Collections;

public class WeaponAnimTest : MonoBehaviour 
{
	public Curve curveA;
	public Curve curveB;
	public float animPos;
	public int curve;
	public float LenghtA;
	public float LenghtB;

	public Transform test;

	void Start () 
	{
		curveA = new Curve(new Vector3(0f, 0f, 0f), new Vector3(-1f, -1f, 0f), new Vector3(-1f, 1f, 0f), new Vector3(0f, 0f, 0f), 100);
		curveB = new Curve(new Vector3(0f, 0f, 0f), new Vector3(1f, -1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(0f, 0f, 0f), 100);
		LenghtA = curveA.lenght;
		LenghtB = curveB.lenght;
		curve = 0;
		animPos = 0f;
	}

	void Update () 
	{
		animPos += Time.deltaTime;
		if(curve == 0)
		{
			test.position = curveA.getDataAt(animPos * LenghtA).pos;
			if(animPos >= 1f)
			{
				animPos -= 1f;
				curve = 1;
				test.position = curveB.getDataAt(animPos * LenghtB).pos;
			}
		}
		else
		{
			test.position = curveB.getDataAt(animPos * LenghtB).pos;
			if(animPos >= 1f)
			{
				animPos -= 1f;
				curve = 0;
				test.position = curveA.getDataAt(animPos * LenghtA).pos;
			}
		}
	}
		
	void OnDrawGizmos()
	{
		if(curveA != null)
		{
			CurveData ccd = curveA.getDataAt(0f);
			CurveData ncd;

			Gizmos.color = new Color(1f, 0.3f, 0.3f, 1f);
			for(int i = 0; i < 20; i++)
			{
				ncd = curveA.getDataAt((i / 20f) * LenghtA);
				Gizmos.DrawLine(ccd.pos, ncd.pos);
				ccd = ncd;
			}
		}

		//Gizmos.DrawLine(ccd.pos, e);
	}
}
