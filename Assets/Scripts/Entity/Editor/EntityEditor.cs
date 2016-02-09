using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Entity))]
public class EntityEditor : Editor
{
	private ReorderableList startingTags;

	private SerializedProperty prop_startingTags;

	protected void OnEnable()
	{
		prop_startingTags = serializedObject.FindProperty("startingTags");

		startingTags = new ReorderableList(serializedObject, prop_startingTags, false, true, true, true);
		startingTags.drawHeaderCallback += OnDrawHeader;
		startingTags.drawElementCallback += OnDrawElement;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.Space();
		startingTags.DoLayoutList();

		serializedObject.ApplyModifiedProperties();
	}

	private void OnDrawHeader(Rect position)
	{
		EditorGUI.LabelField(position, new GUIContent("Tags"));
	}

	private void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
	{
		position.y += 3;
		position.height = EditorGUIUtility.singleLineHeight;

		SerializedProperty element = prop_startingTags.GetArrayElementAtIndex(index);
		EditorGUI.PropertyField(position, element, new GUIContent("Tag " + (index + 1)));
	}
}