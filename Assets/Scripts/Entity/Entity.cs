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

	public Rigidbody Rigidbody
	{
		get
		{
			return GetComponent<Rigidbody>();
		}
	}

	public EntityMovement Movement
	{
		get
		{
			return GetComponent<EntityMovement>();
		}
	}

	public EntityHealth AddonHealth
	{
		get
		{
			return GetComponentInChildren<EntityHealth>();
		}
	}

	public Transform Eyes
	{
		get
		{
			Transform eyes = transform.Find("Eyes");

			if(eyes == null)
			{
				Debug.LogError("[EntityUtils] Entity doesn't have eyes!", this);
			}

			return eyes;
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