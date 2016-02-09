using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	private CameraState currentState;

	public void Transition(CameraState state)
	{
		if(currentState != null)
		{
			currentState.gameObject.SetActive(false);
		}

		currentState = state;
		currentState.MainCamera = Camera.main;

		currentState.gameObject.SetActive(true);
	}
}