using UnityEngine;

public abstract class InputController : MonoBehaviour
{
	public abstract float InputX { get; }

	public abstract float InputZ { get; }

	public abstract bool Jump { get; }

	public abstract bool Crouch { get; }
}