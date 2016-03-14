using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// The state canvas.
/// </summary>
public class StateCanvas : ScriptableObject
{
	[SerializeField] private List<Node> nodes;

	/// <summary>
	/// Get all nodes in this canvas.
	/// </summary>
	public IEnumerable<Node> Nodes
	{
		get
		{
			return nodes;
		}
	}

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Empty State Canvas")]
	private static void CreateEmptyCanvas()
	{
		string path = EditorUtility.SaveFilePanelInProject("Empty Canvas", "StateCanvas", "asset", "Create an empty state canvas.");

		if(!string.IsNullOrEmpty(path))
		{
			StateCanvas canvas = CreateInstance<StateCanvas>();

			AssetDatabase.CreateAsset(canvas, path);
			AssetDatabase.SaveAssets();
		}
	}
#endif
}