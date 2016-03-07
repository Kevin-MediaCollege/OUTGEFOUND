using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour 
{
	public Entity Entity { private set; get; }

	public AIMovement movement;

	protected void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}

	public virtual void init()
	{
	}

	public virtual IEnumerator run()
	{
		yield break;
	}

	public virtual void destory()
	{
	}
}