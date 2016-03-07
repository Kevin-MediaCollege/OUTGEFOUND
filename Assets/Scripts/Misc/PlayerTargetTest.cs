using UnityEngine;
using System.Collections;

public class PlayerTargetTest : MonoBehaviour
{
	protected void FixedUpdate()
	{
		LastKnownPosition.instance.setPosition(transform.position);
	}
}