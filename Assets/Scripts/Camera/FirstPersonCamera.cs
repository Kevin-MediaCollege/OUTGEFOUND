using UnityEngine;
using System.Collections;
using System;

public class FirstPersonCamera : MonoBehaviour
{
	[SerializeField] private Vector2 yConstraint = new Vector2(-70, 70);
	[SerializeField] private Vector2 sensitivity = new Vector2(10, 10);

	private Transform weapon;

	private Entity player;	

	private float yRotation;

	protected void OnEnable()
	{
		player = EntityUtils.GetEntityWithTag("Player");
		weapon = player.transform.Find("Weapon");

		Cursor.lockState = CursorLockMode.Locked;
	}

	protected void LateUpdate()
	{
		// Rotate the player
		player.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity.x, 0);

		yRotation += Input.GetAxis("Mouse Y") * sensitivity.y;
		yRotation = Mathf.Clamp(yRotation, yConstraint.x, yConstraint.y);

		transform.localEulerAngles = new Vector3(-yRotation, transform.localEulerAngles.y, 0);
		weapon.localEulerAngles = new Vector3(-yRotation, weapon.localEulerAngles.y, 0);

		// Move the camera to the player's eyes
		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
	}
}