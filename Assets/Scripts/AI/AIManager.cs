using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : IDependency 
{
	public IEnumerable<Entity> All
	{
		get
		{
			return all;
		}
	}

	private HashSet<Entity> all;
	private LastKnownPosition lastKnownPosition;

	public AIManager()
	{
		all = new HashSet<Entity>();
		lastKnownPosition = Dependency.Get<LastKnownPosition>();
	}

	public void Create()
	{
		GlobalEvents.AddListener<FireEvent>(OnWeaponFireEvent);
		GlobalEvents.AddListener<EntityActivatedEvent>(OnEntityActivatedEvent);
		GlobalEvents.AddListener<EntityDeactivatedEvent>(OnEntityDeactivatedEvent);
	}

	public void Destroy()
	{
		GlobalEvents.AddListener<FireEvent>(OnWeaponFireEvent);
		GlobalEvents.AddListener<EntityActivatedEvent>(OnEntityActivatedEvent);
		GlobalEvents.AddListener<EntityDeactivatedEvent>(OnEntityDeactivatedEvent);

		all.Clear();
	}

	private void OnWeaponFireEvent(FireEvent evt)
	{
		if(evt.Firearm.Wielder.HasTag("Player"))
		{
			lastKnownPosition.Position = evt.Firearm.Wielder.transform.position;
		}
	}

	private void OnEntityActivatedEvent(EntityActivatedEvent evt)
	{
		if(evt.Entity.HasTag("AI"))
		{
			all.Add(evt.Entity);
		}
	}

	private void OnEntityDeactivatedEvent(EntityDeactivatedEvent evt)
	{
		if(evt.Entity.HasTag("AI"))
		{
			all.Remove(evt.Entity);
		}
	}
}
