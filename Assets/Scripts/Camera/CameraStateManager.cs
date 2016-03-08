using UnityEngine;

/// <summary>
/// Manage the MainCamera state, allows for different camera states, such as a first person state and a cutscene state
/// </summary>
public class CameraStateManager : IDependency
{
	private GameObject currentStateObject;

	public void SetToDefaultState()
	{
		SetCurrentState(-1);
	}

	public void SetCurrentState(int state)
	{
		GameObject newstate = CameraStates.GetById(state);

		if(currentStateObject != null)
		{
			Object.Destroy(currentStateObject);
		}

		currentStateObject = Object.Instantiate(newstate);
	}
}