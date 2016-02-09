using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		// Register entity in children
		Component[] interactors = GetComponentsInChildren<Component>(true);
		foreach(Component interactor in interactors)
		{
			IEntityInjector ie = interactor as IEntityInjector;

			if(ie != null)
			{
				ie.RegisterEntity(this);
			}
		}
	}

	protected void OnEnable()
	{
		all.Add(this);
	}

	protected void OnDisable()
	{
		all.Remove(this);
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