using System;
using System.Collections.Generic;

public class ReloadWeaponEvent : IEvent
{
	public Entity Entity { private set; get; }

	public ReloadWeaponEvent(Entity entity)
	{
		Entity = entity;
	}
}