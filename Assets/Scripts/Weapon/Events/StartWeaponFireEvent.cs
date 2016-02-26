using System;
using System.Collections.Generic;

public class StartWeaponFireEvent : IEvent
{
	public Entity Entity { private set; get; }

	public StartWeaponFireEvent(Entity entity)
	{
		Entity = entity;
	}
}