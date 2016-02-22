using UnityEngine;
using System.Collections;

public class CameraStateManager : MonoBehaviour
{
	public static CameraState CurrentState { private set; get; }

	private static CameraStateManager instance;

	[SerializeField] private CameraState defaultState;

	protected void Awake()
	{
		instance = this;
		CurrentState = defaultState;
	}

	protected void LateUpdate()
	{
		CurrentState.ApplyCameraState();
	}

	public static void SetCurrentState(CameraState cameraState)
	{
		if(cameraState == CurrentState)
		{
			return;
		}

		if(CurrentState != null)
		{
			CurrentState.Stop();
		}

		CurrentState = cameraState ?? instance.defaultState;
		CurrentState.Start();
	}
}