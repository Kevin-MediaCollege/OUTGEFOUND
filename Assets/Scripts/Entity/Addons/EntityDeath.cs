using UnityEngine;
using System.Collections;

public class EntityDeath : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }

	protected void OnEnable()
	{
		GlobalEvents.AddListener<EntityDiedEvent>(OnDeathEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<EntityDiedEvent>(OnDeathEvent);
	}

	private void OnDeathEvent(EntityDiedEvent evt)
	{
		if(evt.Entity == Entity)
		{
			Destroy(Entity.gameObject);
		}
	}
}