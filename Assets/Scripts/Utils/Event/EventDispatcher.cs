using System;
using System.Collections.Generic;

public class EventDispatcher : IEventDispatcher
{
	private Dictionary<Type, Action<object>> eventCallbacks;
	private Dictionary<object, Action<object>> delegateLookup;

	public EventDispatcher()
	{
		eventCallbacks = new Dictionary<Type, Action<object>>();
		delegateLookup = new Dictionary<object, Action<object>>();
	}

	public void AddListener(Type type, Action handler)
	{
		if(!eventCallbacks.ContainsKey(type))
		{
			eventCallbacks.Add(type, delegate { });
		}

		Action<object> handlerFunc = obj => { handler(); };

		eventCallbacks[type] += handlerFunc;
		delegateLookup[handler] = handlerFunc;
	}

	public void AddListener<T>(Action<T> handler) where T : IEvent
	{
		Type type = typeof(T);

		if(!eventCallbacks.ContainsKey(type))
		{
			eventCallbacks.Add(type, delegate { });
		}

		Action<object> handlerFunc = obj => { handler((T)obj); };

		eventCallbacks[type] += handlerFunc;
		delegateLookup[handler] = handlerFunc;
	}

	public void RemoveListener(Type type, Action handler)
	{
		if(eventCallbacks.ContainsKey(type) && delegateLookup.ContainsKey(handler))
		{
			eventCallbacks[type] -= delegateLookup[handler];
			delegateLookup.Remove(handler);

			if(eventCallbacks[type].GetInvocationList().Length == 1)
			{
				eventCallbacks.Remove(type);
			}
		}
	}

	public void RemoveListener<T>(Action<T> handler) where T : IEvent
	{
		Type type = typeof(T);

		if(eventCallbacks.ContainsKey(type) && delegateLookup.ContainsKey(handler))
		{
			eventCallbacks[type] -= delegateLookup[handler];
			delegateLookup.Remove(handler);

			if(eventCallbacks[type].GetInvocationList().Length == 1)
			{
				eventCallbacks.Remove(type);
			}
		}
	}

	public void RemoveAllListeners()
	{
		eventCallbacks.Clear();
		delegateLookup.Clear();
	}

	public void Invoke(Type type, object evt)
	{
		Action<object> handler;

		if(eventCallbacks.TryGetValue(type, out handler))
		{
			if(handler != null)
			{
				handler.Invoke(evt);
			}
		}
	}

	public void Invoke<T>(T evt) where T : IEvent
	{
		Type type = typeof(T);
		Invoke(type, evt);
	}
}
