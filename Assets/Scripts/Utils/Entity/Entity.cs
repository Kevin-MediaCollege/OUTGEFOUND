using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocalEvents))]
public class Entity : MonoBehaviour
{
	public static IEnumerable<Entity> All
	{
		get
		{
			return all;
		}
	}

	private static HashSet<Entity> all = new HashSet<Entity>();

	private LocalEvents events;
	public IEventDispatcher Events
	{
		get
		{
			if(events == null)
			{
				events = GetComponent<LocalEvents>();
			}

			return events;
		}
	}

	[SerializeField] private string[] startingTags;

	private HashSet<string> tags;

	#region Unity Callbacks
	protected void Awake()
	{
		tags = new HashSet<string>();

		// Add the starting tags
		foreach(string tag in startingTags)
		{
			tags.Add(tag);
		}
	}

	protected void OnEnable()
	{
		all.Add(this);

		GlobalEvents.Invoke(new EntityActivatedEvent(this));
	}

	protected void OnDisable()
	{
		all.Remove(this);

		GlobalEvents.Invoke(new EntityDeactivatedEvent(this));
	}
	#endregion

	public void AddTag(string tag)
	{
		tags.Add(tag);
	}

	public void RemoveTag(string tag)
	{
		tags.Remove(tag);
	}

	public bool HasTag(string tag)
	{
		return tags.Contains(tag);
	}
}