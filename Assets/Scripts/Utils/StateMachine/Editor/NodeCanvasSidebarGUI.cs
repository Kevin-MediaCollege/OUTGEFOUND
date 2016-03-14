using UnityEditor;

/// <summary>
/// The sidebar GUI
/// </summary>
public class NodeCanvasSidebarGUI
{
	public const float SIDEBAR_WIDTH = 250f;

	private NodeCanvasNodeSidebarGUI nodeSidebarGUI;
	private NodeCanvasLinkSidebarGUI linkSidebarGUI;

	private SerializedObject canvas;

	private NodeData prevNode;
	private NodeData node;

	public NodeCanvasSidebarGUI(SerializedObject canvas)
	{
		this.canvas = canvas;
	}

	public void Draw()
	{
		node = NodeProvider.Instance.SelectedNode;

		if(node != null)
		{
			if(node != prevNode)
			{
				Initialize();
			}

			nodeSidebarGUI.Draw();
			linkSidebarGUI.Draw();
		}
	}

	private void Initialize()
	{
		prevNode = node;

		nodeSidebarGUI = new NodeCanvasNodeSidebarGUI(canvas, node);
		linkSidebarGUI = new NodeCanvasLinkSidebarGUI(canvas);
	}
}