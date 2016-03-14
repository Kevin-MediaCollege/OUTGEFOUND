using UnityEngine;

/// <summary>
/// Test to get AI to see the player
/// </summary>
public class PlayerTargetTest : MonoBehaviour
{
	private LastKnownPosition lastKnownPosition;

	protected void Awake()
	{
		lastKnownPosition = Dependency.Get<LastKnownPosition>();
	}

	protected void FixedUpdate()
	{
		lastKnownPosition.Position = transform.position;
	}
}