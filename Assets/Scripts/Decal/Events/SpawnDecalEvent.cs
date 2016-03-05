using System.Collections;
using UnityEngine;

public class SpawnDecalEvent : IEvent
{
	public Vector3 Position { private set; get; }

	public Vector3 Normal { private set; get; }

	public SpawnDecalEvent(Vector3 position, Vector3 normal)
	{
		Position = position;
		Normal = normal;
	}
}
