using System.Collections;
using UnityEngine;

/// <summary>
/// Player movement controller
/// </summary>
public class PlayerMovement : EntityMovement
{
	[SerializeField] private float adsSpeed;

	[SerializeField] private CharacterController characterController;
	[SerializeField] private PlayerInputController playerInputController;

	[SerializeField] private CapsuleCollider bulletCollider;

	[SerializeField] private float standingHeight;
	[SerializeField] private float crouchingHeight;

	private float tweenStart;
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

	protected void Update()
	{
		if(characterController.isGrounded)
		{
			verticalSpeed = 0;

			if(playerInputController.Jump)
			{
				verticalSpeed = jumpSpeed;
			}
		}

		// Toggle crouching
		if(playerInputController.Crouch)
		{
			Crouching = !Crouching;

			StopCoroutine("HandleCrouch");
			StartCoroutine("HandleCrouch");
		}
	}

	protected void FixedUpdate()
	{
		Vector3 velocityX = transform.right * playerInputController.InputX * (ads ? adsSpeed : speed);
		Vector3 velocityZ = transform.forward * playerInputController.InputZ * (ads ? adsSpeed : speed);

		verticalSpeed += Physics.gravity.y * Time.deltaTime;

		Vector3 velocity = velocityX + velocityZ;
		velocity.y = verticalSpeed;

		characterController.Move(velocity * Time.deltaTime);

		// Play footstep audio
		if(characterController.velocity.magnitude > 0.3f)
		{
			PlayFootstep();
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

	private IEnumerator HandleCrouch()
	{
		float height = Crouching ? crouchingHeight : standingHeight;
		tweenStart = Time.time;

		float target = 0;
		while(target != height)
		{
			target = Mathf.Lerp(characterController.height, height, (Time.time - tweenStart) * 2);

			characterController.height = target;
			bulletCollider.height = target;

			yield return null;
		}
	}
}