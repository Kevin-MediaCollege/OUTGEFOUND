using UnityEngine;

/// <summary>
/// The EntityAddon retrieves a reference to it's parent Entity
/// </summary>
public abstract class EntityAddon : MonoBehaviour
{
	public Entity Entity { private set; get; }

	protected virtual void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}
}
