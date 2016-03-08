public class EntityDiedEvent : IEvent
{
	public DamageInfo DamageInfo { private set; get; }

	public Entity Entity { private set; get; }

	public EntityDiedEvent(DamageInfo damageInfo, Entity entity)
	{
		DamageInfo = damageInfo;
		Entity = entity;
	}
}
