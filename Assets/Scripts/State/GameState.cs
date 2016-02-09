using UnityEngine;
using System.Collections;

public abstract class GameState : MonoBehaviour
{
	public CameraController CameraController { set; get; }

	public GameStateManager GameStateManager { set; get; }
}