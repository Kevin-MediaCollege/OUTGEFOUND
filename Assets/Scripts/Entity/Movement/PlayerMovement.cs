using UnityEngine;

/// <summary>
/// Player movement controller
/// </summary>
public class PlayerMovement : EntityMovement
{
	[SerializeField] private float adsSpeed;

	[SerializeField] private CharacterController characterController;
	[SerializeField] private PlayerInputController playerInputController;

	private bool ads;

	protected void OnEnable()
	{
		GlobalEvents.AddListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		GlobalEvents.AddListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		GlobalEvents.RemoveListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	protected void FixedUpdate()
	{
		Vector3 velocityX = transform.right * playerInputController.InputX * (ads ? adsSpeed : speed);
		Vector3 velocityZ = transform.forward * playerInputController.InputZ * (ads ? adsSpeed : speed);

		if(characterController.isGrounded)
		{
			verticalSpeed = playerInputController.Jump ? jumpSpeed : 0;
		}
		else
		{
			verticalSpeed += Physics.gravity.y * Time.deltaTime;
		}

		Vector3 velocity = velocityX + velocityZ;
		velocity.y = verticalSpeed;

		characterController.Move(velocity * Time.deltaTime);

		// Play footstep audio
		if(characterController.velocity.magnitude > 0.3f)
		{
			PlayFootstep();
		}

		// Toggle crouching
		if(playerInputController.Crouch)
		{
			Crouching = !Crouching;
		}
	}

	private void OnStartAimDownSightEvent(StartAimDownSightEvent evt)
	{
		ads = true;
	}

	private void OnStopAimDownSightEvent(StopAimDownSightEvent evt)
	{
		ads = false;
	}
}