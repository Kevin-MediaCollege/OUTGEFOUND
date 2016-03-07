using UnityEngine;
using System.Collections;

public class LastKnownPosition : MonoBehaviour 
{
	public static LastKnownPosition instance;

	private Vector3 position;
	private bool seen;

	void Awake()
	{
		instance = this;
		seen = false;
	}

	public bool hasBeenSeen()
	{
		return seen;
	}

	public void setPosition(Vector3 _pos)
	{
		position = _pos;
		seen = true;
	}

	public Vector3 getPosition()
	{
		return position;
	}

	public void disappear()
	{
		seen = false;
	}
}
