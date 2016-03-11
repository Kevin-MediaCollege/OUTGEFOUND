using UnityEngine;

/// <summary>
/// The canvas header GUI for the node editor.
/// </summary>
public class NodeCanvasControlGUI
{
	/// <summary>
	/// Draw the control GUI.
	/// </summary>
	public void Draw(Rect position)
	{
		if(GUI.Button(new Rect(5, position.height - 30, 110, 25), "Add Node"))
		{
			NodeProvider.Instance.SelectedNode = NodeProvider.Instance.AddNode();
		}

		// Remove node button.
		if(NodeProvider.Instance.SelectedNode != null)
		{
			if(GUI.Button(new Rect(120, position.height - 30, 110, 25), "Remove Node"))
			{
				NodeProvider.Instance.RemoveNode(NodeProvider.Instance.SelectedNode);
			}
		}
	}
}