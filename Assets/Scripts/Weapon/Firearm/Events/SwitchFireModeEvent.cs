using System;
using System.Collections.Generic;

public class SwitchFireModeEvent : IEvent
{
	public Entity Entity { private set; get; }

	public SwitchFireModeEvent(Entity entity)
	{
		Entity = entity;
	}
}