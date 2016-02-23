using UnityEngine;
using System.Collections;

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