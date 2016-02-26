using System;
using System.Collections.Generic;

public class MagazineEmptyEvent : IEvent
{
	public Weapon Weapon { private set; get; }

	public MagazineEmptyEvent(Weapon weapon)
	{
		Weapon = weapon;
	}
}