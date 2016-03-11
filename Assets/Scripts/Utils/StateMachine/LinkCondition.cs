using UnityEngine;

/// <summary>
/// The base interface of any condition in the state machine.
/// </summary>
public abstract class LinkCondition : ScriptableObject
{
	/// <summary>
	/// Called whenever the condition is created.
	/// </summary>
	public virtual void Create() { }

	/// <summary>
	/// Called whenever the condition is destroyed.
	/// </summary>
	public virtual void Destroy() { }

	/// <summary>
	/// Is the current condition valid or not? If this returns true, the state machine will continue to the next state.
	/// </summary>
	public abstract bool IsValid { get; }
}