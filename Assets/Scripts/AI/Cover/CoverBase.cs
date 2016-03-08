using UnityEngine;
using System.Collections;

public class CoverBase : MonoBehaviour 
{
	public Vector2 Size
	{
		get
		{
			return new Vector2(coverSizeX, coverSizeY);
		}
	}

	public Entity Occupant { set; get; }

	public float CoverAngle
	{
		get
		{
			return coverAngle;
		}
	}

	public float Angle
	{
		get
		{
			return transform.eulerAngles.y;
		}
	}

	public bool IsSafe { set; get; }

	public bool IsUsefull { set; get; }

	public bool IsOccupied
	{
		get
		{
			return Occupant != null;
		}
	}

	[Header("Height of the cover.")]
	[Range(0.2f, 3f)]
	[SerializeField] private float coverSizeX;

	[Header("Width of the cover.")]
	[Range(0.2f, 3f)]
	[SerializeField] private float coverSizeY;

	[Header("Distance from player position to cover")]
	[Range(0.3f, 1.5f)]
	[SerializeField] private float coverOffset;

	[Header("Safe to take cover angle")]
	[Range(10f, 90f)]
	[SerializeField] private float coverAngle;

	protected void Awake()
	{
		Dependency.Get<CoverManager>().AddCover(this);
	}

	protected void OnDrawGizmosSelected()
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

		Gizmos.color = new Color(1f, 0f, 0f);
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, coverSizeY + 0.5f, 0f));
	}
}