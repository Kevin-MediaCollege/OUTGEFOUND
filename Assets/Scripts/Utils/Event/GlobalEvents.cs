using System;

public class GlobalEvents
{
	private static IEventDispatcher eventDispacher;

	static GlobalEvents()
	{
		eventDispacher = new EventDispatcher();
	}

	public static void AddListener(Type type, Action handler)
	{
		eventDispacher.AddListener(type, handler);
	}

	public static void AddListener<T>(Action<T> handler) where T : IEvent
	{
		eventDispacher.AddListener(handler);
	}

	public static void RemoveListener(Type type, Action handler)
	{
		eventDispacher.RemoveListener(type, handler);
	}

	public static void RemoveListener<T>(Action<T> handler) where T : IEvent
	{
		eventDispacher.RemoveListener(handler);
	}

	public static void Invoke(Type type, object evt)
	{
		eventDispacher.Invoke(type, evt);
	}

	public static void Invoke<T>(T evt) where T : IEvent
	{
		eventDispacher.Invoke(evt);
	}
}
