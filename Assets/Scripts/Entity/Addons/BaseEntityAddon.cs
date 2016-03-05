using UnityEngine;

public abstract class BaseEntityAddon : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }
}
