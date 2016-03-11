using UnityEngine;
using UnityEditor;

/// <summary>
/// The base of the node editor window.
/// </summary>
public class NodeEditorWindow : EditorWindow
{
	public static NodeEditorWindow Instance { private set; get; }

	private SerializedObject canvas;

	private NodeCanvasGUI canvasGUI;
	private NodeCanvasControlGUI controlGUI;
	private NodeCanvasSidebarGUI canvasSidebarGUI;

	private NodeProvider nodeProvider;

	#region Unity callbacks
	protected void OnEnable()
	{
		Instance = this;
	} 

	protected void OnGUI()
	{
		if(canvas == null)
		{
			FindCanvas(null);

			if(canvas == null)
			{
				return;
			}
		}

		canvas.Update();
		nodeProvider.PreUpdate();

		GUILayout.BeginHorizontal();

		Rect rect = GetRemainingWindow();
		canvasGUI.Draw(rect);

		GUILayout.BeginVertical();
		canvasSidebarGUI.Draw();
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		controlGUI.Draw(position);

		nodeProvider.PostUpdate();
		canvas.ApplyModifiedProperties();

		Repaint();
	}

	protected void OnSelectionChange()
	{
		FindCanvas(null);
		Repaint();
	}
	#endregion

	/// <summary>
	/// Assign the editor window to a canvas.
	/// </summary>
	/// <param name="canvas"></param>
	public void Assign(StateCanvas canvas)
	{
		this.canvas = new SerializedObject(canvas);

		nodeProvider = new NodeProvider(this.canvas);

		canvasGUI = new NodeCanvasGUI();
		controlGUI = new NodeCanvasControlGUI();
		canvasSidebarGUI = new NodeCanvasSidebarGUI(this.canvas);
	}

	/// <summary>
	/// Try to find the canvas from the currently selected (game) object.
	/// </summary>
	/// <param name="canvas"></param>
	private void FindCanvas(StateCanvas canvas)
	{
		this.canvas = null;

		if(canvas == null)
		{
			if(Selection.activeGameObject != null)
			{
				StateMachine stateMachine = Selection.activeGameObject.GetComponent<StateMachine>();

				if(stateMachine != null)
				{
					canvas = new SerializedObject(stateMachine).FindProperty("canvas").objectReferenceValue as StateCanvas;
				}
			}
			else if(Selection.activeObject != null)
			{
				if(Selection.activeObject is StateCanvas)
				{
					canvas = Selection.activeObject as StateCanvas;
				}
				else
				{
					string path = AssetDatabase.GetAssetPath(Selection.activeObject);

					if(!string.IsNullOrEmpty(path))
					{
						canvas = AssetDatabase.LoadAssetAtPath(path, typeof(StateCanvas)) as StateCanvas;
					}
				}
			}
		}

		if(canvas != null)
		{
			Assign(canvas);
		}
	}

	private Rect GetRemainingWindow()
	{
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		return GUILayoutUtility.GetLastRect();
	}

	[MenuItem("Window/Node Editor")]
	public static void OpenNodeEditor()
	{
		NodeEditorWindow window = GetWindow<NodeEditorWindow>();
		window.minSize = new Vector2(720, 405);
		window.titleContent = new GUIContent("Node Editor");
	}
}