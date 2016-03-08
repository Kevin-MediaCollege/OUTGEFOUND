using UnityEngine;

/// <summary>
/// Base input controller, used to move the entity
/// </summary>
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