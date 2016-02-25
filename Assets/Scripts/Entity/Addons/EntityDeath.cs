using UnityEngine;
using System.Collections;

public class EntityDeath : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }

	protected void OnEnable()
	{
		GlobalEvents.AddListener<EntityDeathEvent>(OnDeathEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<EntityDeathEvent>(OnDeathEvent);
	}

	private void OnDeathEvent(EntityDeathEvent evt)
	{
		if(evt.DamageInfo.Hit.Target == Entity)
		{
			Destroy(Entity.gameObject);
		}
	}
}