public class WeaponFireEvent : IEvent
{
	public Weapon Weapon { private set; get; }

	public HitInfo HitInfo { private set; get; }

	public WeaponFireEvent(Weapon weapon, HitInfo hitInfo)
	{
		Weapon = weapon;
		HitInfo = hitInfo;
	}
}
