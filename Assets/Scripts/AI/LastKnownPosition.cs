using UnityEngine;
using System.Collections;

public class LastKnownPosition : IDependency 
{
	public bool Seen { set; get; }

	private Vector3 position;
	public Vector3 Position
	{
		set
		{
			position = value;
			Seen = true;
		}
		get
		{
			return position;
		}
	}
}
