using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The first-person camera behaviour
/// </summary>
public class GameCamera : MonoBehaviour
{
	[SerializeField] private Vector2 yConstraint;
	[SerializeField] private Vector2 sensitivity;

	private Transform weapon;
	private Transform player;
	private Transform eyes;

	private float yRotation;

	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	protected void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void Update()
	{
		if(Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}

	protected void FixedUpdate()
	{
		if(player == null || weapon == null || eyes == null)
		{
			Entity p = EntityUtils.GetEntityWithTag("Player");

			if(p == null)
			{
				Debug.LogError("No player found");
				return;
			}

			player = p.transform;
			weapon = player.Find("Weapon");
			eyes = player.Find("Eyes");
		}

		// Rotate the player
		player.Rotate(0, (Input.GetAxis("Mouse X") * sensitivity.x) * Time.deltaTime, 0);

		yRotation += (Input.GetAxis("Mouse Y") * sensitivity.y) * Time.deltaTime;
		yRotation = Mathf.Clamp(yRotation, yConstraint.x, yConstraint.y);

		eyes.localEulerAngles = new Vector3(-yRotation, transform.localEulerAngles.y, 0);
		weapon.localEulerAngles = new Vector3(-yRotation, weapon.localEulerAngles.y, 0);

		// Move the camera to the player's eyes
		Camera.main.transform.position = eyes.position;
		Camera.main.transform.rotation = eyes.rotation;
	}
}