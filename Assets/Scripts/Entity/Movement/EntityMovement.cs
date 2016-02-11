using UnityEngine;
using System.Collections;

public class EntityMovement : MonoBehaviour
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

	[SerializeField] private CharacterController characterController;
	[SerializeField] private InputController input;

	[SerializeField] private float speed;
	[SerializeField] private float jumpSpeed;

	private float verticalSpeed;

	protected void Update()
	{
		Vector3 velocityX = transform.right * input.InputX * speed;
		Vector3 velocityZ = transform.forward * input.InputZ * speed;

		if(characterController.isGrounded)
		{
			verticalSpeed = input.Jump ? jumpSpeed : 0;
		}
		else
		{
			verticalSpeed += Physics.gravity.y * Time.deltaTime;
		}		

		Vector3 velocity = velocityX + velocityZ;
		velocity.y = verticalSpeed;

		characterController.Move(velocity * Time.deltaTime);
	}
}