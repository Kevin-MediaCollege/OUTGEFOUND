using UnityEngine;
using System.Collections;

public abstract class AIBase : MonoBehaviour 
{
	public Entity Entity { private set; get; }

	public AIMovement Movement
	{
		get
		{
			return movement;
		}
	}

	[SerializeField] private AIMovement movement;

	protected virtual void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}

	public abstract IEnumerator Run();

	public bool MoveTo(Vector3 point)
	{
		return movement.MoveTo(point);
	}

	public void Stop()
	{
		movement.Stop();
	}
}