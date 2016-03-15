using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The first-person camera behaviour
/// </summary>
public class FirstPersonCamera : MonoBehaviour
{
	[SerializeField] private Vector2 yConstraint = new Vector2(-70, 70);
	[SerializeField] private Vector2 sensitivity = new Vector2(10, 10);

	private Transform weapon;
	private Transform player;
	private Transform eyes;

	private float yRotation;

	protected void OnEnable()
	{
		player = EntityUtils.GetEntityWithTag("Player").transform;

		weapon = player.Find("Weapon");
		eyes = player.Find("Eyes");		

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	protected void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void FixedUpdate()
	{
		Firearm firearm = player.GetComponentInChildren<Firearm>();

		// Rotate the player
		player.Rotate(0, Input.GetAxis("Mouse X") * sensitivity.x, 0);

		yRotation += Input.GetAxis("Mouse Y") * sensitivity.y;
		yRotation = Mathf.Clamp(yRotation, yConstraint.x, yConstraint.y);

		eyes.localEulerAngles = new Vector3(-yRotation, transform.localEulerAngles.y, 0);
		weapon.localEulerAngles = new Vector3(-yRotation, weapon.localEulerAngles.y, 0);

		// Move the camera to the player's eyes
		Camera.main.transform.position = eyes.position;
		Camera.main.transform.rotation = eyes.rotation;

		if(firearm != null && firearm.Firing)
		{
			Vector3 shake = UnityEngine.Random.insideUnitCircle * 0.15f;
			Camera.main.transform.position += shake;
		}		
	}
}