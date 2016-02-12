using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Firearm))]
public class FirearmEditor : Editor
{
	private SerializedProperty prop_barrel;

	private SerializedProperty prop_gunShot;
	private SerializedProperty prop_gunShotPitchRange;

	private SerializedProperty prop_muzzleFlash;
	private SerializedProperty prop_muzzleFlashDisplayDuration;

	private SerializedProperty prop_fireModes;

	private SerializedProperty prop_shotsPerBurst;

	protected void OnEnable()
	{
		prop_barrel = serializedObject.FindProperty("barrel");

		prop_gunShot = serializedObject.FindProperty("gunShot");
		prop_gunShotPitchRange = serializedObject.FindProperty("gunShotPitchRange");

		prop_muzzleFlash = serializedObject.FindProperty("muzzleFlash");
		prop_muzzleFlashDisplayDuration = serializedObject.FindProperty("muzzleFlashDisplayDuration");

		prop_fireModes = serializedObject.FindProperty("fireModes");

		prop_shotsPerBurst = serializedObject.FindProperty("shotsPerBurst");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(prop_barrel);
		prop_fireModes.intValue = (int)((Firearm.FireMode)EditorGUILayout.EnumMaskPopup(new GUIContent("Available Fire Modes"), (Firearm.FireMode)prop_fireModes.intValue));

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Gunshot Audio", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(prop_gunShot, new GUIContent("Audio"));
		EditorGUILayout.PropertyField(prop_gunShotPitchRange, new GUIContent("Min/Max Pitch"));

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Muzzle Flash", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(prop_muzzleFlash, new GUIContent("Renderer"));
		EditorGUILayout.PropertyField(prop_muzzleFlashDisplayDuration, new GUIContent("Display Duration"));

		if(((Firearm.FireMode)prop_fireModes.intValue & Firearm.FireMode.Burst) == Firearm.FireMode.Burst)
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Burst Fire Mode Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(prop_shotsPerBurst);
		}

		serializedObject.ApplyModifiedProperties();
	}
}