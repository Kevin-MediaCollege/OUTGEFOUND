using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeCanvasLinkSidebarGUI
{
	public static SerializedObject selectedLink;

	private SerializedObject canvas;

	public NodeCanvasLinkSidebarGUI(SerializedObject canvas)
	{
		this.canvas = canvas;
	}

	public void Draw()
	{
		if(selectedLink != null && selectedLink.targetObject != null)
		{
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);

			DrawConditions();
		}
	}

	private void DrawConditions()
	{
		List<Type> allConditions = new List<Type>(Reflection.AllTypesFrom<LinkCondition>());

		EditorGUILayout.LabelField("Active Conditions");
		foreach(LinkCondition condition in (selectedLink.targetObject as Link).Conditions)
		{
			allConditions.Remove(condition.GetType());
			DrawCondition(condition.GetType(), false);
		}

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Available Conditions");
		foreach(Type type in allConditions)
		{
			DrawCondition(type, true);
		}
	}

	private void DrawCondition(Type type, bool canAdd)
	{
		GUILayout.BeginHorizontal();

		EditorGUILayout.LabelField(type.Name, EditorStyles.boldLabel, GUILayout.Width(NodeCanvasSidebarGUI.SIDEBAR_WIDTH - 25));

		if(canAdd)
		{
			if(GUILayout.Button("+", GUILayout.Width(20)))
			{
				AddCondition(type);
			}
        }
		else
		{
			if(GUILayout.Button("-", GUILayout.Width(20)))
			{
				RemoveCondition(type);
			}

			if(GUILayout.Button("S", GUILayout.Width(20)))
			{
				SelectCondition(type);
			}
		}

		GUILayout.EndHorizontal();
	}

	private void AddCondition(Type type)
	{
		Link link = selectedLink.targetObject as Link;

		LinkCondition condition = ScriptableObject.CreateInstance(type) as LinkCondition;
		condition.name = "LinkCondition (" + link.From.Name + " => " + link.To.Name + " (" + type.Name + "))";

		AssetDatabase.AddObjectToAsset(condition, canvas.targetObject);
		AssetDatabase.SaveAssets();

		SerializedProperty conditions = selectedLink.FindProperty("conditions");
		conditions.InsertArrayElementAtIndex(conditions.arraySize);
		conditions.GetArrayElementAtIndex(conditions.arraySize - 1).objectReferenceValue = condition;
		selectedLink.ApplyModifiedProperties();
	}

	private void RemoveCondition(Type type)
	{
		foreach(LinkCondition condition in (selectedLink.targetObject as Link).Conditions)
		{
			if(condition.GetType() == type)
			{
				SerializedProperty conditions = selectedLink.FindProperty("conditions");

				for(int i = 0; i < conditions.arraySize; i++)
				{
					SerializedProperty element = conditions.GetArrayElementAtIndex(i);

					if(element.objectReferenceValue.GetType() == type)
					{
						element.objectReferenceValue = null;
						conditions.DeleteArrayElementAtIndex(i);
						selectedLink.ApplyModifiedProperties();
						break;
					}
				}

				UnityEngine.Object.DestroyImmediate(condition, true);
				AssetDatabase.SaveAssets();
				break;
			}
		}
	}

	private void SelectCondition(Type type)
	{
		foreach(LinkCondition condition in (selectedLink.targetObject as Link).Conditions)
		{
			if(condition.GetType() == type)
			{
				Selection.activeObject = condition;
				break;
			}
		}
	}
}