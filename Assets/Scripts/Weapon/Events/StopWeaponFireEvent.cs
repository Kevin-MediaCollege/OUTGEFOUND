using System;
using System.Collections.Generic;

public class StopWeaponFireEvent : IEvent
{
	public Entity Entity { private set; get; }

	public StopWeaponFireEvent(Entity entity)
	{
		Entity = entity;
	}
}