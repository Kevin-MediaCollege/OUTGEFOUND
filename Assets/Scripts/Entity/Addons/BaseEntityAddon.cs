using UnityEngine;

public abstract class BaseEntityAddon : MonoBehaviour
{
	public Entity Entity { private set; get; }

	protected virtual void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}
}
