/// <summary>
/// The ICommunicant interface is an interface you can implement if you want your <see cref="Utils.Dependencies.IStateDependency"/>
/// or <see cref="UnityEngine.MonoBehaviour"/> in a state to be able to send and receive events on the state machine channel.
/// </summary>
public interface ICommunicant
{
	/// <summary>
	/// Register the event dispatcher.
	/// This is called whenever the dependency or state has been created.
	/// </summary>
	/// <param name="eventDispatcher">The state machine's event dispatcher.</param>
	void RegisterEventDispatcher(IEventDispatcher eventDispatcher);
}