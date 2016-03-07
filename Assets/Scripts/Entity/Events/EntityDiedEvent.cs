public class EntityDiedEvent : IEvent
{
	public Entity Entity { private set; get; }

	public EntityDiedEvent(Entity entity)
	{
		Entity = entity;
	}
}
