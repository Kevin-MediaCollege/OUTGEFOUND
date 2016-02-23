using System;
using System.Collections.Generic;

public struct DamageInfo
{
	public Entity Source { private set; get; }
	public Entity Target { private set; get; }

	public int Damage { set; get; }

	public DamageInfo(Entity source, Entity target, int damage)
	{
		Source = source;
		Target = target;

		Damage = damage;
	}
}