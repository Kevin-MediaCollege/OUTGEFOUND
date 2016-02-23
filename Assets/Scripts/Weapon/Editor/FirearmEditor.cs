using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Firearm))]
public class FirearmEditor : Editor
{
	private SerializedProperty prop_barrel;

	private SerializedProperty prop_fireModes;
	private SerializedProperty prop_magazine;

	private SerializedProperty prop_shotsPerBurst;

	protected void OnEnable()
	{
		prop_barrel = serializedObject.FindProperty("barrel");

		prop_fireModes = serializedObject.FindProperty("fireModes");
		prop_magazine = serializedObject.FindProperty("magazine");

		prop_shotsPerBurst = serializedObject.FindProperty("shotsPerBurst");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(prop_barrel);
		prop_fireModes.intValue = (int)((Firearm.FireMode)EditorGUILayout.EnumMaskPopup(new GUIContent("Available Fire Modes"), (Firearm.FireMode)prop_fireModes.intValue));

		EditorGUILayout.PropertyField(prop_magazine);

		if(((Firearm.FireMode)prop_fireModes.intValue & Firearm.FireMode.Burst) == Firearm.FireMode.Burst)
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Burst Fire Mode Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(prop_shotsPerBurst);
		}

		serializedObject.ApplyModifiedProperties();
	}
}