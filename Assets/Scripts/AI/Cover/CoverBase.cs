using UnityEngine;
using System.Collections;

public class CoverBase : MonoBehaviour 
{
	[Header("Height of the cover.")]
	[Range(0.2f, 3f)]
	public float coverSizeX;

	[Header("Width of the cover.")]
	[Range(0.2f, 3f)]
	public float coverSizeY;

	[Header("Distance from player position to cover")]
	[Range(0.3f, 1.5f)]
	public float coverOffset;

	[Header("Safe to take cover angle")]
	[Range(10f, 90f)]
	public float coverAngle;

	[HideInInspector]
	public bool isSafe;

	void Awake()
	{
		
	}

	void Start()
	{
		CoverManager.instance.addCover (this);
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

		Gizmos.color = new Color(0f, 0f, 1f);
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(Mathf.Sin((gameObject.transform.eulerAngles.y + coverAngle) * Mathf.PI / 180f) * 5f, 0f, Mathf.Cos((gameObject.transform.eulerAngles.y + coverAngle) * Mathf.PI / 180f) * 5f));
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(Mathf.Sin((gameObject.transform.eulerAngles.y - coverAngle) * Mathf.PI / 180f) * 5f, 0f, Mathf.Cos((gameObject.transform.eulerAngles.y - coverAngle) * Mathf.PI / 180f) * 5f));
	}

	public float getAngle()
	{
		return gameObject.transform.eulerAngles.y;
	}
}
