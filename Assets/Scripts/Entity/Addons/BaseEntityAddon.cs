using UnityEngine;

/// <summary>
/// The BaseEntityAddon retrieves a reference to it's parent Entity
/// </summary>
public abstract class BaseEntityAddon : MonoBehaviour
{
	public Entity Entity { private set; get; }

	protected virtual void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}
}
