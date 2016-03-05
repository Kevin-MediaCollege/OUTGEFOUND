using System;

public interface IEventDispatcher
{
	void AddListener(Type type, Action handler);

	void AddListener<T>(Action<T> handler) where T : IEvent;

	void RemoveListener(Type type, Action handler);

	void RemoveListener<T>(Action<T> handler) where T : IEvent;

	void Invoke(Type type, object evt);

	void Invoke<T>(T evt) where T : IEvent;
}
