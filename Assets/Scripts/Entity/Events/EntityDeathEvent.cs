using System;
using System.Collections.Generic;

public class EntityDeathEvent : IEvent
{
	public DamageInfo DamageInfo { private set; get; }

	public EntityDeathEvent(DamageInfo damageInfo)
	{
		DamageInfo = damageInfo;
	}
}