using UnityEngine;
using System.Collections;

public class PlayerMovement : EntityMovement
{
	[SerializeField] private CharacterController characterController;

	protected void Awake()
	{
		Dependency.Get<CameraStateManager>().SetToDefaultState();
	}

	protected void FixedUpdate()
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