public class StartFireEvent : IEvent
{
}

public class StopFireEvent : IEvent
{
}

public class WeaponFireEvent : IEvent
{
	public Weapon Weapon { private set; get; }

	public HitInfo Hit { private set; get; }

	public WeaponFireEvent(Weapon weapon, HitInfo hit)
	{
		Weapon = weapon;
		Hit = hit;
	}
}

public class WeaponDamageEvent : IEvent
{
	public Weapon Weapon { private set; get; }

	public DamageInfo Damage { private set; get; }

	public HitInfo Hit
	{
		get
		{
			return Damage.hit;
		}
	}

	public WeaponDamageEvent(Weapon weapon, DamageInfo damage)
	{
		Weapon = weapon;
		Damage = damage;
	}
}