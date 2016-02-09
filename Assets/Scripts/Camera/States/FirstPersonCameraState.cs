using UnityEngine;
using System.Collections;
using System;

public class FirstPersonCameraState : CameraState
{
	[SerializeField] private Vector2 yConstraint = new Vector2(-70, 70);
	[SerializeField] private Vector2 sensitivity = new Vector2(10, 10);

	private Transform eyes;
	private Entity player;	

	private Vector2 rotation;

	protected void OnEnable()
	{
		player = EntityUtils.GetEntityWithTag("Player");
		eyes = player.transform.Find("Eyes");

		Cursor.lockState = CursorLockMode.Locked;
	}

	protected void Update()
	{
		// Move the camera to the player's eyes
		MainCamera.transform.position = eyes.position;
		MainCamera.transform.rotation = eyes.rotation;

		// Rotate the player
		rotation.x += Input.GetAxis("Mouse X") * sensitivity.x;
		rotation.y += Input.GetAxis("Mouse Y") * sensitivity.y;		
		rotation.y = Mathf.Clamp(rotation.y, yConstraint.x, yConstraint.y);

		Quaternion xQuaternion = Quaternion.AngleAxis(rotation.x, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis(rotation.y, Vector3.left);

		player.transform.localRotation = xQuaternion;
		//MainCamera.transform.localRotation = yQuaternion;
	}
}