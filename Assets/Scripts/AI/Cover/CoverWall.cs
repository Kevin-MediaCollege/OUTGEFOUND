using UnityEngine;
using System.Collections;

public class CoverWall : CoverBase 
{
	public float Angle { get { return angle; } }

	[Header("Safe to take cover angle")]
	[Range(10f, 90f)]
	[SerializeField] private float angle = 40f;

	protected void Awake()
	{
		Dependency.Get<CoverManager>().AddCover(this);
	}

	public override void UpdateCover(Vector3 _playerPos, Vector3 _playerHead)
	{
		IsUsefull = true;
		IsSafe = true;
		Vector3 start = transform.position + new Vector3(0f, Size.y + 0.5f, 0f);
		float dist = Vector3.Distance(start, _playerHead);

		if(dist < 5f)
		{
			IsSafe = false;
			IsUsefull = false;
			return;
		}

		IsSafe = Mathf.Abs ((CoverRotation - RotationHelper.fixRotation (CoverRotation, 
			RotationHelper.rotationToPoint (gameObject.transform.position, _playerPos)))) < Angle ? true : false;

		if(!IsSafe)
		{
			IsUsefull = false;
			return;
		}

		IsUsefull = true;
		RaycastHit[] hits = Physics.RaycastAll(start, _playerHead - start, dist);
		int l = hits.Length;
		for(int i = 0; i < l; i++)
		{
			if(hits[i].collider.gameObject.CompareTag("Wall"))
			{
				IsUsefull = false;
				break;
			}
		}
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
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(Mathf.Sin((gameObject.transform.eulerAngles.y + angle) * Mathf.PI / 180f) * 5f, 0f, Mathf.Cos((gameObject.transform.eulerAngles.y + angle) * Mathf.PI / 180f) * 5f));
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(Mathf.Sin((gameObject.transform.eulerAngles.y - angle) * Mathf.PI / 180f) * 5f, 0f, Mathf.Cos((gameObject.transform.eulerAngles.y - angle) * Mathf.PI / 180f) * 5f));

		Gizmos.color = new Color(0f, 1f, 0f);
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, coverSizeY + 0.5f, 0f));
	}
}
