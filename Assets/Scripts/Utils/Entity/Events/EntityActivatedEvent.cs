public class EntityActivatedEvent : IEvent
{
	public Entity Entity { private set; get; }

	public EntityActivatedEvent(Entity entity)
	{
		Entity = entity;
	}
}