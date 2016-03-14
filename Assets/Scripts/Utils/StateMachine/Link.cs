using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A link in the state machine.
/// </summary>
public class Link : ScriptableObject
{
	[SerializeField] private Node from;
	[SerializeField] private Node to;

	[SerializeField] private LinkCondition[] conditions = new LinkCondition[0];

	/// <summary>
	/// Set/Get the source node of this link.
	/// </summary>
	public Node From
	{
		set
		{
			from = value;
		}
		get
		{
			return from;
		}
	}

	/// <summary>
	/// Set/Get the destination node of this link.
	/// </summary>
	public Node To
	{
		set
		{
			to = value;
		}
		get
		{
			return to;
		}
	}

	/// <summary>
	/// Get the type of the condition of this link.
	/// </summary>
	public IEnumerable<LinkCondition> Conditions
	{
		get
		{
			return conditions;
		}
	}
}