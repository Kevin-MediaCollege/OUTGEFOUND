using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A node in the state machine.
/// </summary>
public class Node : ScriptableObject
{
	[SerializeField] private List<Link> links;

	[SerializeField] private Vector2 position;

	[SerializeField] private string sceneName;
	[SerializeField] private string[] additionalScenes;

	[SerializeField] private string nodeName = "New Node";

	[SerializeField] private bool entryPoint;

	/// <summary>
	/// Get the links of this node.
	/// </summary>
	public IEnumerable<Link> Links
	{
		get
		{
			return links;
		}
	}

	/// <summary>
	/// Get the position in the canvas of this node.
	/// </summary>
	public Vector2 Position
	{
		set
		{
			position = value;
		}
		get
		{
			return position;
		}
	}

	/// <summary>
	/// Get the state prefab of this node.
	/// </summary>
	public string SceneName
	{
		get
		{
			return sceneName;
		}
	}

	public IEnumerable<string> AdditionalScenes
	{
		get
		{
			return additionalScenes;
		}
	}

	/// <summary>
	/// Get the name of this node.
	/// </summary>
	public string Name
	{
		get
		{
			return nodeName;
		}
	}

	/// <summary>
	/// Is this node the entry point of the state machine?
	/// </summary>
	/// <remarks>
	/// There can only be one entry point in each state machine.
	/// </remarks>
	public bool EntryPoint
	{
		get
		{
			return entryPoint;
		}
	}
		
	/// <summary>
	/// Add a link to this node.
	/// </summary>
	/// <param name="link">The link to add.</param>
	/// <returns>Whether or not the link has been added.</returns>
	public bool AddLink(Link link)
	{
		if(links == null)
		{
			links = new List<Link>();
		}

		if(links.Contains(link))
		{
			return false;
		}
			
		links.Add(link);
		return true;
	}

	/// <summary>
	/// Remove a link from this node.
	/// </summary>
	/// <param name="link">The link to remove.</param>
	/// <returns>Whether or not the link has been removed.</returns>
	public bool RemoveLink(Link link)
	{
		if(links.Contains(link))
		{
			return links.Remove(link);
		}

		return false;
	}
}