using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// 
///
///
public class NodeCanvasNodeSidebarGUI
{
	public static SerializedObject selectingLink;

	private ReorderableList rl_links;

	private SerializedProperty prop_sceneName;
	private SerializedProperty prop_additionalScenes;
	private SerializedProperty prop_links;

	private SerializedObject canvas;

	private NodeData node;

	public NodeCanvasNodeSidebarGUI(SerializedObject canvas, NodeData node)
	{
		this.canvas = canvas;
		this.node = node;

		prop_sceneName = node.serializedObject.FindProperty("sceneName");
		prop_additionalScenes = node.serializedObject.FindProperty("additionalScenes");
		prop_links = node.serializedObject.FindProperty("links");

		rl_links = new ReorderableList(node.serializedObject, prop_links, false, true, true, true);
		rl_links.drawHeaderCallback += LinksHeaderCallback;
		rl_links.drawElementCallback += LinksElementCallback;
		rl_links.onAddCallback += LinksAddCallback;
		rl_links.onRemoveCallback += LinksRemoveCallback;
		rl_links.onSelectCallback += LinksSelectCallback;
	}

	public void Draw()
	{
		//GUI.Box(position, "");

		EditorGUIUtility.labelWidth = 70;

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Node", EditorStyles.boldLabel);

		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(node.serializedObject.FindProperty("nodeName"), new GUIContent("Name"), GUILayout.Width(NodeCanvasSidebarGUI.SIDEBAR_WIDTH));
		GUILayout.EndHorizontal();

		EditorGUILayout.PropertyField(node.serializedObject.FindProperty("entryPoint"));

		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(prop_sceneName, new GUIContent("Scene"));
		EditorGUILayout.PropertyField(prop_additionalScenes, true);

		EditorGUILayout.Separator();
		rl_links.DoLayoutList();

		GUILayout.EndVertical();
	}

	#region ReorderableList header
	private void LinksHeaderCallback(Rect rect)
	{
		EditorGUI.LabelField(rect, "Links");
	}
	#endregion

	#region ReorderableList element
	private void LinksElementCallback(Rect rect, int index, bool active, bool focused)
	{
		SerializedObject element = new SerializedObject(prop_links.GetArrayElementAtIndex(index).objectReferenceValue);

		SerializedProperty from = element.FindProperty("from");
		SerializedProperty to = element.FindProperty("to");

		Rect contentRect = new Rect(rect);
		contentRect.y += 3;
		contentRect.height = EditorGUIUtility.singleLineHeight;

		contentRect.width = rect.width * 0.40f;
		EditorGUI.LabelField(contentRect, (from.objectReferenceValue as Node).Name);

		contentRect.x += contentRect.width;
		contentRect.width = rect.width * 0.2f;
		EditorGUI.LabelField(contentRect, "=>");

		contentRect.x += contentRect.width;
		contentRect.width = rect.width * 0.4f;
		if(to.objectReferenceValue != null)
		{
			EditorGUI.LabelField(contentRect, (to.objectReferenceValue as Node).Name);
		}
		else
		{
			if(selectingLink == null)
			{
				if(GUI.Button(contentRect, "Select"))
				{
					selectingLink = element;
				}
			}
		}
	}
	#endregion

	#region ReorderableList add
	private void LinksAddCallback(ReorderableList rl)
	{
		int index = rl.serializedProperty.arraySize;

		Link link = ScriptableObject.CreateInstance<Link>();
		link.From = node.node;
		link.name = "Link";

		AssetDatabase.AddObjectToAsset(link, canvas.targetObject);
		AssetDatabase.SaveAssets();

		rl.serializedProperty.InsertArrayElementAtIndex(index);
		rl.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue = link;
	}
	#endregion

	#region ReorderableList remove
	private void LinksRemoveCallback(ReorderableList rl)
	{
		SerializedProperty array = rl.serializedProperty;
		SerializedProperty element = array.GetArrayElementAtIndex(rl.index);

		Link link = element.objectReferenceValue as Link;

		foreach(LinkCondition condition in link.Conditions)
		{
			UnityEngine.Object.DestroyImmediate(condition, true);
		}

		UnityEngine.Object.DestroyImmediate(link, true);
		AssetDatabase.SaveAssets();

		element.objectReferenceValue = null;
		array.DeleteArrayElementAtIndex(rl.index);
		array.serializedObject.ApplyModifiedProperties();

		selectingLink = null;
		NodeCanvasLinkSidebarGUI.selectedLink.Dispose();
		NodeCanvasLinkSidebarGUI.selectedLink = null;
	}
	#endregion

	#region ReorderableList select
	private void LinksSelectCallback(ReorderableList rl)
	{
		if(NodeCanvasLinkSidebarGUI.selectedLink != null)
		{
			NodeCanvasLinkSidebarGUI.selectedLink.ApplyModifiedProperties();
			NodeCanvasLinkSidebarGUI.selectedLink.Dispose();
			NodeCanvasLinkSidebarGUI.selectedLink = null;
		}

		NodeCanvasLinkSidebarGUI.selectedLink = new SerializedObject(rl.serializedProperty.GetArrayElementAtIndex(rl.index).objectReferenceValue);
	}
	#endregion
}