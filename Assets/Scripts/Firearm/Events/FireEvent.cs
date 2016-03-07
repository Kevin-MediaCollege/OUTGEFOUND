public class FireEvent : IEvent
{
	public Firearm Firearm { private set; get; }

	public HitInfo HitInfo { private set; get; }

	public FireEvent(Firearm firearm, HitInfo hitInfo)
	{
		Firearm = firearm;
		HitInfo = hitInfo;
	}
}
