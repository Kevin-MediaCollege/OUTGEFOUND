using System;
using UnityEngine;

/// <summary>
/// A simple condition which listens for the specified event.
/// </summary>
public class EventReceivedCondition : LinkCondition, ICommunicant
{
	[TypeDropdown(typeof(IStateEvent))][SerializeField] private string @event;

	private IEventDispatcher eventDispatcher;

	private bool isValid;
	public override bool IsValid
	{
		get
		{
			return isValid;
		}
	}

		
	public override void Create()
	{
		isValid = false;
	}

	public override void Destroy()
	{
		eventDispatcher.RemoveListener(Type.GetType(@event), OnEventReceived);
	}

	public void RegisterEventDispatcher(IEventDispatcher eventDispatcher)
	{
		this.eventDispatcher = eventDispatcher;
		this.eventDispatcher.AddListener(Type.GetType(@event), OnEventReceived);
	}

	/// <summary>
	/// Event handler.
	/// </summary>
	private void OnEventReceived()
	{
		isValid = true;
	}
}