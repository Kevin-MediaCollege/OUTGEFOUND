using UnityEngine;
using System.Collections;

public class PlayerTargetTest : MonoBehaviour
{
	private LastKnownPosition lastKnownPosition;

	protected void Awake()
	{
		lastKnownPosition = Dependency.Get<LastKnownPosition>();
	}

	protected void FixedUpdate()
	{
		//lastKnownPosition.Position = transform.position;
	}
}