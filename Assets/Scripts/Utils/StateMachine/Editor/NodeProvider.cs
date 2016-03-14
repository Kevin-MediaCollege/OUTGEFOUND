using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A container for node data.
/// </summary>
public class NodeData
{
	public SerializedObject serializedObject;
	public Node node;

	public Vector2? dragPosition;
}

public class NodeProvider
{
	public static NodeProvider Instance { private set; get; }

	/// <summary>
	/// Get all nodes.
	/// </summary>
	public IEnumerable<NodeData> Nodes
	{
		get
		{
			return nodeData;
		}
	}

	public NodeData SelectedNode { set; get; }

	private HashSet<NodeData> nodeData;
	private HashSet<NodeData> toRemove;

	private SerializedObject canvas;
	private SerializedProperty nodes;

	public NodeProvider(SerializedObject canvas)
	{
		Instance = this;

		this.canvas = canvas;
		this.toRemove = new HashSet<NodeData>();

		nodeData = new HashSet<NodeData>();
		nodes = canvas.FindProperty("nodes");

		// Create data for existing nodes.
		for(int i = 0; i < nodes.arraySize; i++)
		{
			SerializedProperty node = nodes.GetArrayElementAtIndex(i);

			NodeData data = new NodeData();
			data.serializedObject = new SerializedObject(node.objectReferenceValue);
			data.node = node.objectReferenceValue as Node;

			nodeData.Add(data);
		}
	}

	/// <summary>
	/// Add a node.
	/// </summary>
	/// <returns>The data of the node.</returns>
	public NodeData AddNode()
	{
		Node node = ScriptableObject.CreateInstance<Node>();
		node.name = "Node";

		AssetDatabase.AddObjectToAsset(node, canvas.targetObject);
		AssetDatabase.SaveAssets();

		int arrayLocation = nodes.arraySize;

		nodes.InsertArrayElementAtIndex(arrayLocation);

		SerializedProperty result = nodes.GetArrayElementAtIndex(arrayLocation);			
		result.objectReferenceValue = node;
		result.serializedObject.ApplyModifiedProperties();

		NodeData data = new NodeData();
		data.serializedObject = new SerializedObject(node);
		data.node = node;

		nodeData.Add(data);

		data.serializedObject.FindProperty("position").vector2Value = new Vector2(100, 100);

		if(arrayLocation == 0)
		{
			data.serializedObject.FindProperty("entryPoint").boolValue = true;
		}

		return data;
	}

	/// <summary>
	/// Remove a node.
	/// </summary>
	/// <param name="node">The data of the node to remove.</param>
	public void RemoveNode(NodeData node)
	{
		if(!toRemove.Contains(node))
		{
			toRemove.Add(node);
		}
	}

	/// <summary>
	/// Update the serialized objects of all nodes.
	/// </summary>
	public void PreUpdate()
	{
		foreach(NodeData node in nodeData)
		{
			node.serializedObject.Update();
		}
	}

	/// <summary>
	/// Apply the modified properties of all nodes, and remove all nodes scheduled for removal.
	/// </summary>
	public void PostUpdate()
	{
		foreach(NodeData node in nodeData)
		{
			node.serializedObject.ApplyModifiedProperties();
		}
			
		foreach(NodeData node in toRemove)
		{
			for(int i = 0; i < nodes.arraySize; i++)
			{
				SerializedProperty current = nodes.GetArrayElementAtIndex(i);

				if(current.objectReferenceValue == node.node)
				{
					if(SelectedNode != null)
					{
						if(node.node == SelectedNode.node)
						{
							SelectedNode = null;
						}
					}

					nodeData.Remove(node);

					foreach(Link link in node.node.Links)
					{
						foreach(LinkCondition condition in link.Conditions)
						{
							Object.DestroyImmediate(condition, true);
						}

						Object.DestroyImmediate(link, true);
					}

					foreach(NodeData data in nodeData)
					{
						List<Link> linksToRemove = new List<Link>();

						foreach(Link link in data.node.Links)
						{
							if(link.To == node.node || link.From == node.node)
							{
								foreach(LinkCondition condition in link.Conditions)
								{
									Object.DestroyImmediate(condition, true);
								}

								linksToRemove.Add(link);
								Object.DestroyImmediate(link, true);
							}
						}

						foreach(Link link in linksToRemove)
						{
							data.node.RemoveLink(link);
						}
					}

					Object.DestroyImmediate(node.node, true);

					current.objectReferenceValue = null;
					nodes.DeleteArrayElementAtIndex(i);
					break;
				}
			}
		}

		nodes.serializedObject.ApplyModifiedProperties();
		AssetDatabase.SaveAssets();

		toRemove.Clear();
	}
}