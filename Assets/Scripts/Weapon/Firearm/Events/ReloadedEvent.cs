using System;
using System.Collections.Generic;

public class ReloadedEvent : IEvent
{
	public Weapon Weapon { private set; get; }

	public ReloadedEvent(Weapon weapon)
	{
		Weapon = weapon;
	}
}