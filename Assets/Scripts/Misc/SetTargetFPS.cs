using UnityEngine;

/// <summary>
/// Set the target FPS
/// </summary>
public class SetTargetFPS : MonoBehaviour
{
	[SerializeField] private int targetFrameRate;

	protected void Awake()
	{
		Application.targetFrameRate = targetFrameRate;
	}
}