using UnityEngine;
using System.Collections;

public abstract class CameraState : MonoBehaviour
{
	public abstract void Start();

	public abstract void Stop();

	public abstract void ApplyCameraState();
}