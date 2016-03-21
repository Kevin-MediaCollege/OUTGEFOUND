using UnityEngine;
using System.Collections;

public class CoverBase : MonoBehaviour 
{
	protected Vector2 Size { get { return new Vector2(coverSizeX, coverSizeY); } }
	public Entity Occupant { set; get; }
	public float CoverRotation { get { return this != null ? transform.eulerAngles.y : 0; } }
	public bool IsSafe { set; get; }
	public bool IsUsefull { set; get; }
	public bool IsOccupied { get { return Occupant != null; } }
	public Vector3 RayPosition { get { return rayPosition; } }

	[Header("Height of the cover.")]
	[Range(0.2f, 3f)]
	[SerializeField] protected float coverSizeX = 0.7f;

	[Header("Width of the cover.")]
	[Range(0.2f, 3f)]
	[SerializeField] protected float coverSizeY = 1f;

	[Header("Distance from player position to cover")]
	[Range(0.3f, 1.5f)]
	[SerializeField] protected float coverOffset = 0.8f;

	protected Vector3 rayPosition;

	public virtual void UpdateCover(Vector3 _playerPos, Vector3 _playerHead)
	{
	}
}