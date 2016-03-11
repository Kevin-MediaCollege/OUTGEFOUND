/// <summary>
/// A collection of commonly used ICommunicant utilities.
/// </summary>
public static class ICommunicantUtils
{
	/// <summary>
	/// Attempt to add an IEventDispatcher to one or more objects.
	/// </summary>
	/// <param name="eventDispatcher">the event dispatcher.</param>
	/// <param name="objects">The objects.</param>
	public static void RegisterEventDispatcher(IEventDispatcher eventDispatcher, params object[] objects)
	{
		foreach(object obj in objects)
		{
			if(obj is ICommunicant)
			{
				(obj as ICommunicant).RegisterEventDispatcher(eventDispatcher);
			}
		}
	}
}