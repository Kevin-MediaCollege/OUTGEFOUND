using UnityEngine;

public abstract class InputController : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }

	public abstract bool Jump { get; }

	public abstract bool Crouch { get; }
}