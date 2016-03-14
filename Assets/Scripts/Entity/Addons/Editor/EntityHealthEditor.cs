using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntityHealth))]
public class EntityHealthEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if(Application.isPlaying)
		{
			EntityHealth health = serializedObject.targetObject as EntityHealth;

			GUI.enabled = false;
			EditorGUILayout.Separator();
			EditorGUILayout.TextField(new GUIContent("Current Health"), health.CurrentHealth.ToString());
			GUI.enabled = true;
		}
	}
}