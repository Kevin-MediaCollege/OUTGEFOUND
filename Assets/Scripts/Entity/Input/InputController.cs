using UnityEngine;

public abstract class InputController : MonoBehaviour
{
	public Entity Entity { private set; get; }

	public abstract bool Jump { get; }

	public abstract bool Crouch { get; }

	protected void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}
}