using UnityEngine;
using System.Collections;

public class CoverBase : MonoBehaviour 
{
	public Vector2 coverSize;
	public float coverOffset;

	void OnDrawGizmosSelected()
	{
		Vector3 left = gameObject.transform.position + (gameObject.transform.forward * coverOffset) + (gameObject.transform.right * coverSize.x);
		Vector3 right = gameObject.transform.position + (gameObject.transform.forward * coverOffset) + (-(gameObject.transform.right * coverSize.x));
		Vector3 height = new Vector3(0f, coverSize.y, 0f);

		Gizmos.color = new Color(1f, 0f, 0f);
		Gizmos.DrawLine(left, right);
		Gizmos.DrawLine(left, left + height);
		Gizmos.DrawLine(right, right + height);
		Gizmos.DrawLine(left + height, right + height);
	}
}
