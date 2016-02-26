public class EntityDiedEvent : IEvent
{
	public Entity Entity { private set; get; }

	public DamageInfo DamageInfo { private set; get; }

	public EntityDiedEvent(DamageInfo damageInfo)
	{
		DamageInfo = damageInfo;
		Entity = DamageInfo.Hit.Target;
	}
}