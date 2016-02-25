public class DamageEvent : IEvent
{
	public DamageInfo DamageInfo { private set; get; }

	public DamageEvent(DamageInfo damageInfo)
	{
		DamageInfo = damageInfo;
	}
}