using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
	[SerializeField] private CameraController cameraController;
	[SerializeField] private GameState startState;

	private GameState currentState;

	protected void OnEnable()
	{
		Transition(startState);
	}

	public void Transition(GameState state)
	{
		if(currentState != null)
		{
			currentState.gameObject.SetActive(false);
		}

		currentState = state;
		currentState.CameraController = cameraController;
		currentState.GameStateManager = this;

		currentState.gameObject.SetActive(true);
	}
}