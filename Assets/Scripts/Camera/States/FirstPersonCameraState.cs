using UnityEngine;
using System.Collections;
using System;

public class FirstPersonCameraState : CameraState
{
	[SerializeField] private Vector2 yConstraint = new Vector2(-70, 70);
	[SerializeField] private Vector2 sensitivity = new Vector2(10, 10);

	private Transform eyes;
	private Transform weapon;

	private Entity player;	

	private float yRotation;

	protected void OnEnable()
	{
		player = EntityUtils.GetEntityWithTag("Player");

		eyes = player.transform.Find("Eyes");
		weapon = player.transform.Find("Weapon");

		Cursor.lockState = CursorLockMode.Locked;
	}

	protected void LateUpdate()
	{
		// Rotate the player
		player.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity.x, 0);

		yRotation += Input.GetAxis("Mouse Y") * sensitivity.y;
		yRotation = Mathf.Clamp(yRotation, yConstraint.x, yConstraint.y);

		eyes.localEulerAngles = new Vector3(-yRotation, eyes.localEulerAngles.y, 0);
		weapon.localEulerAngles = new Vector3(-yRotation, weapon.localEulerAngles.y, 0);

		// Move the camera to the player's eyes
		MainCamera.transform.position = eyes.position;
		MainCamera.transform.rotation = eyes.rotation;
	}
}