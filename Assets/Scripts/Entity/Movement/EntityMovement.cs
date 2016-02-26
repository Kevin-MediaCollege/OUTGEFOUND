using UnityEngine;
using System.Collections;

public abstract class EntityMovement : MonoBehaviour
{
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
}