using UnityEditor;
using UnityEngine;

/// <summary>
/// The node canvas GUI.
/// This is responsible for drawing the canvas GUI.
/// </summary>
public class NodeCanvasGUI
{
	public const float NODE_WIDTH = 180;
	public const float NODE_HEADER_HEIGHT = 20;
	public const float NODE_CONTENT_HEIGHT = 51;

	private bool startDrag;
	private bool dragging;

	/// <summary>
	/// Draw the canvas.
	/// </summary>
	public void Draw(Rect rect)
	{
		GUI.Box(rect, "");

		// Update the selected node.
		UpdateSelectedNode();

		// Draw the links between all nodes.
		// This is a seperate loop because link lines will be above the nodes otherwise.
		foreach(NodeData node in NodeProvider.Instance.Nodes)
		{
			DrawLinks(node);
		}

		// Draw all nodes.
		foreach(NodeData node in NodeProvider.Instance.Nodes)
		{
			Bounds bounds = GetNodeBounds(node.node);

			// Attempt to drag the node.
			if(NodeProvider.Instance.SelectedNode != null)
			{
				if(NodeProvider.Instance.SelectedNode.node == node.node)
				{
					UpdateDragState(node, bounds);

					if(dragging)
					{
						Vector2 targetPosition = Event.current.mousePosition;
						targetPosition.x -= NODE_WIDTH / 2;
						targetPosition.y -= (NODE_HEADER_HEIGHT + NODE_CONTENT_HEIGHT - 1) / 2;

						node.dragPosition = targetPosition;
					}
				}
			}

			// Draw the node.
			DrawNode(node);
		}
	}

	/// <summary>
	/// Draw a single node.
	/// </summary>
	/// <param name="node">The node to draw.</param>
	private void DrawNode(NodeData node)
	{
		Vector2 nodePosition = node.dragPosition == null ? node.node.Position : node.dragPosition.Value;
			
		Vector2 headerPosition = nodePosition;
		Vector2 contentPosition = new Vector2(headerPosition.x, headerPosition.y + NODE_HEADER_HEIGHT - 1);

		// Header box
		Rect position = new Rect(headerPosition, new Vector2(NODE_WIDTH, NODE_HEADER_HEIGHT));
		GUI.Box(position, node.node.Name + (node.node.EntryPoint ? " (ENTRY POINT)" : ""));

		// Content box
		position = new Rect(contentPosition, new Vector2(NODE_WIDTH, NODE_CONTENT_HEIGHT));
		GUI.Box(position, "");
	}

	/// <summary>
	/// Draw all links comming out of a single node.
	/// </summary>
	/// <param name="node">The node.</param>
	private void DrawLinks(NodeData node)
	{
		Handles.BeginGUI();

		// Determine the starting position.
		Vector2 fromPosition = node.node.Position;

		if(dragging && node.dragPosition != null)
		{
			fromPosition = node.dragPosition.Value;
		}

		// Center the starting position.
		fromPosition.x += NODE_WIDTH / 2;
		fromPosition.y += (NODE_HEADER_HEIGHT + NODE_CONTENT_HEIGHT - 1) / 2;

		foreach(Link link in node.node.Links)
		{
			if(link != null)
			{
				if(link.From != null && link.To != null)
				{
					// Determine the target position.
					Vector2 toPosition = link.To.Position;

					if(dragging)
					{
						foreach(NodeData node2 in NodeProvider.Instance.Nodes)
						{
							if(node2.node == link.To)
							{
								if(node2.dragPosition != null)
								{
									toPosition = node2.dragPosition.Value;
								}
							}
						}
					}

					// Center the ending position.
					toPosition.x += NODE_WIDTH / 2;
					toPosition.y += (NODE_HEADER_HEIGHT + NODE_CONTENT_HEIGHT - 1) / 2;

					// Draw the line.

					Handles.color = Color.black;
					Handles.DrawLine(fromPosition, toPosition);
				}
			}
		}

		Handles.EndGUI();
	}

	/// <summary>
	/// Update the selected node.
	/// </summary>
	private void UpdateSelectedNode()
	{
		if(Event.current.type == EventType.MouseDown)
		{
			if(Event.current.button == 0)
			{
				Vector2 mousePosition = Event.current.mousePosition;

				foreach(NodeData node in NodeProvider.Instance.Nodes)
				{
					Bounds bounds = GetNodeBounds(node.node);

					if(bounds.Contains(mousePosition))
					{
						if(NodeCanvasNodeSidebarGUI.selectingLink != null)
						{
							NodeCanvasNodeSidebarGUI.selectingLink.FindProperty("to").objectReferenceValue = node.node;
							NodeCanvasNodeSidebarGUI.selectingLink.ApplyModifiedProperties();
							NodeCanvasNodeSidebarGUI.selectingLink = null;
						}
						else
						{
							NodeProvider.Instance.SelectedNode = node;
							NodeCanvasLinkSidebarGUI.selectedLink = null;
						}

						break;
					}
				}
			}
			else if(Event.current.button == 1)
			{
				if(NodeCanvasNodeSidebarGUI.selectingLink != null)
				{
					NodeCanvasNodeSidebarGUI.selectingLink = null;
				}
				else
				{
					NodeProvider.Instance.SelectedNode = null;
				}					
			}
		}
	}

	/// <summary>
	/// Update the drag state.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <param name="bounds">The bounds of the node.</param>
	private void UpdateDragState(NodeData node, Bounds bounds)
	{
		// "startDrag" creates a delay so the node doesn't move instantly when you press it.

		if(startDrag)
		{
			if(Event.current.type == EventType.MouseDrag && Event.current.button == 0)
			{
				dragging = true;
				startDrag = false;
			}
			else if(Event.current.type == EventType.MouseUp && Event.current.button == 0)
			{
				startDrag = false;
			}
		}

		if(!dragging)
		{
			if(Event.current.type == EventType.MouseDown && Event.current.button == 0)
			{
				Vector2 mousePosition = Event.current.mousePosition;

				if(bounds.Contains(mousePosition))
				{
					startDrag = true;
				}
			}
		}
		else
		{
			if(Event.current.type == EventType.MouseUp && Event.current.button == 0)
			{
				node.serializedObject.FindProperty("position").vector2Value = node.dragPosition.Value;
				node.dragPosition = null;
				dragging = false;
			}
		}
	}

	/// <summary>
	/// Get the bounds of a node.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <returns>The bounds of a node.</returns>
	private Bounds GetNodeBounds(Node node)
	{
		float totalWidth = NODE_WIDTH;
		float totalHeight = NODE_HEADER_HEIGHT + NODE_CONTENT_HEIGHT - 1;

		Bounds bounds = new Bounds();
		bounds.center = new Vector2(node.Position.x + (totalWidth / 2), node.Position.y + (totalHeight / 2));
		bounds.extents = new Vector2(totalWidth / 2, totalHeight / 2);

		return bounds;
	}
}