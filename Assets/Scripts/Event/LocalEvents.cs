using System;
using UnityEngine;

public class LocalEvents : MonoBehaviour, IEventDispatcher
{
	private IEventDispatcher eventDispatcher;
	private IEventDispatcher EventDispatcher
	{
		get
		{
			if(eventDispatcher == null)
			{
				eventDispatcher = new EventDispatcher();
			}

			return eventDispatcher;
		}
	}

	public void AddListener(Type type, Action handler)
	{
		EventDispatcher.AddListener(type, handler);
	}

	public void AddListener<T>(Action<T> handler) where T : IEvent
	{
		EventDispatcher.AddListener(handler);
	}

	public void RemoveListener(Type type, Action handler)
	{
		EventDispatcher.RemoveListener(type, handler);
	}

	public void RemoveListener<T>(Action<T> handler) where T : IEvent
	{
		EventDispatcher.RemoveListener(handler);
	}

	public void Invoke(Type type, object evt)
	{
		EventDispatcher.Invoke(type, evt);
	}

	public void Invoke<T>(T evt) where T : IEvent
	{
		EventDispatcher.Invoke(evt);
	}
}
