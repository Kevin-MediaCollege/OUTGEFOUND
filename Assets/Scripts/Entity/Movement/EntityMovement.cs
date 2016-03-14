using UnityEngine;

/// <summary>
/// Base movement controller
/// </summary>
public abstract class EntityMovement : MonoBehaviour
{
	public Entity Entity { private set; get; }

	public bool Jumping
	{
		get
		{
			return verticalSpeed > 0;
		}
	}

	public bool Falling
	{
		get
		{
			return verticalSpeed < 0;
		}
	}

	public bool Crouching
	{
		get
		{
			return crouching;
		}
	}

	[SerializeField] protected float speed;
	[SerializeField] protected float jumpSpeed;

	protected float verticalSpeed;

	protected bool crouching;

	protected virtual void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}
}