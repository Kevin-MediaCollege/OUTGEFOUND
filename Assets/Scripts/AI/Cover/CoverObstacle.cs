using UnityEngine;
using System.Collections;

public class CoverObstacle : CoverBase 
{
	public float AngleOutside { get { return angleOutside; } }
	public float AngleInside { get { return angleInside; } }
	public bool AngleSide { get { return side; } }
	public Vector3 ShootPosition { get { return shootPosition; } }

	[Header("Safe to take cover angle Outside")]
	[Range(10f, 90f)]
	[SerializeField] private float angleOutside = 60f;

	[Header("Safe to take cover angle Inside")]
	[Range(10f, 90f)]
	[SerializeField] private float angleInside = 20f;

	[Header("Shoot position")]
	[Range(0.1f, 1.4f)]
	[SerializeField] private float shootOffset = 0.55f;

	[Header("Left or Right side? right = true, left = false")]
	[SerializeField] private bool side = true;

	private Vector3 shootPosition;

	protected void Awake()
	{
		Dependency.Get<CoverManager>().AddCover(this);

		shootPosition = gameObject.transform.position + ((side ? gameObject.transform.right : -gameObject.transform.right) * shootOffset * 2f);
		rayPosition = shootPosition + new Vector3(0f, 1.5f, 0f);
	}

	public override void UpdateCover(Vector3 _playerPos, Vector3 _playerHead)
	{
		IsUsefull = true;
		IsSafe = true;
		float dist = Vector3.Distance(rayPosition, _playerHead);

		if(dist < 3f)
		{
			IsSafe = false;
			IsUsefull = false;
			return;
		}

		float rotationToPlayer = CoverRotation - RotationHelper.fixRotation (CoverRotation, RotationHelper.rotationToPoint (shootPosition, _playerPos));
		if ((side ? !(angleInside > rotationToPlayer && -angleOutside < rotationToPlayer) : !(-angleInside < rotationToPlayer && angleOutside > rotationToPlayer))) 
		{
			IsSafe = false;
		}

		if(!IsSafe)
		{
			IsUsefull = false;
			return;
		}

		IsUsefull = true;
		RaycastHit[] hits = Physics.RaycastAll(rayPosition, _playerHead - rayPosition, dist);
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
		Vector3 player = new Vector3(0f, 1.5f, 0f);
		Vector3 shootpos = gameObject.transform.position + ((side ? gameObject.transform.right : -gameObject.transform.right) * shootOffset * 2f);

		Gizmos.color = new Color(1f, 0f, 0f);
		Gizmos.DrawLine(left, right);
		Gizmos.DrawLine(left, left + height);
		Gizmos.DrawLine(right, right + height);
		Gizmos.DrawLine(left + height, right + height);

		Gizmos.color = new Color(0f, 1f, 0f);
		Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + player);
		Gizmos.DrawLine(gameObject.transform.position, shootpos);
		Gizmos.DrawLine(gameObject.transform.position + player, shootpos + player);

		Gizmos.color = new Color(0f, 0f, 1f);
		Gizmos.DrawLine(shootpos, shootpos + new Vector3(Mathf.Sin((gameObject.transform.eulerAngles.y + (side ? angleOutside : angleInside)) * Mathf.PI / 180f) * 5f, 0f, Mathf.Cos((gameObject.transform.eulerAngles.y + (side ? angleOutside : angleInside)) * Mathf.PI / 180f) * 5f));
		Gizmos.DrawLine(shootpos, shootpos + new Vector3(Mathf.Sin((gameObject.transform.eulerAngles.y - (side ? angleInside : angleOutside)) * Mathf.PI / 180f) * 5f, 0f, Mathf.Cos((gameObject.transform.eulerAngles.y - (side ? angleInside : angleOutside)) * Mathf.PI / 180f) * 5f));
	}
}
