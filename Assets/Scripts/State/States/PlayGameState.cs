using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameState : GameState
{
	[SerializeField] private CameraState firstPersonCamera;

	protected void OnEnable()
	{
		CameraController.Transition(firstPersonCamera);
	}
}