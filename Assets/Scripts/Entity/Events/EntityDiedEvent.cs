public class EntityDiedEvent : IEvent
{
	public DamageInfo Damage { private set; get; }

	public Entity Entity { private set; get; }

	public EntityDiedEvent(DamageInfo damageInfo, Entity entity)
	{
		Damage = damageInfo;
		Entity = entity;
	}
}
