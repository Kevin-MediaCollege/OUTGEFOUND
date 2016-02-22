using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{
	[SerializeField] private MonoBehaviour[] interactiveBehaviours;

	public void DisableInteraction()
	{
		foreach(MonoBehaviour behaviour in interactiveBehaviours)
		{
			behaviour.enabled = false;
		}
	}

	public void EnableInteraction()
	{
		foreach(MonoBehaviour behaviour in interactiveBehaviours)
		{
			behaviour.enabled = true;
		}
	}
}