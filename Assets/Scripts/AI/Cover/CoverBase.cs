using UnityEngine;
using System.Collections;

public class CoverBase : MonoBehaviour 
{
	[Range(0.2f, 3f)]
	public float coverSizeX;

	[Range(0.2f, 3f)]
	public float coverSizeY;

	[Range(0.3f, 1.5f)]
	public float coverOffset;

	void Awake()
	{
		
	}

	void OnDrawGizmosSelected()
	{
		Vector3 left = gameObject.transform.position + (gameObject.transform.forward * coverOffset) + (gameObject.transform.right * coverSizeX);
		Vector3 right = gameObject.transform.position + (gameObject.transform.forward * coverOffset) + (-(gameObject.transform.right * coverSizeX));
		Vector3 height = new Vector3(0f, coverSizeY, 0f);

		Gizmos.color = new Color(1f, 0f, 0f);
		Gizmos.DrawLine(left, right);
		Gizmos.DrawLine(left, left + height);
		Gizmos.DrawLine(right, right + height);
		Gizmos.DrawLine(left + height, right + height);
	}

	public float getAngle()
	{
		return gameObject.transform.eulerAngles.y;
	}
}
