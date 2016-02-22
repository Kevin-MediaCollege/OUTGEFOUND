using UnityEngine;
using System.Collections;
using System;

public class StoreCamera : CameraState
{
	public override void Start()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	public override void ApplyCameraState()
	{
		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
	}

	public override void Stop()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}