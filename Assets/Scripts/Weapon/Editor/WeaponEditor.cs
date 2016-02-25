using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon), true)]
public class WeaponEditor : Editor
{
	private SerializedProperty prop_baseUpgrade;
	private SerializedProperty prop_upgradeData;

	protected void OnEnable()
	{
		prop_baseUpgrade = serializedObject.FindProperty("baseUpgrade");
		prop_upgradeData = serializedObject.FindProperty("upgradeData");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(!Application.isPlaying)
		{
			EditorGUILayout.Separator();

			DrawButton();
		}
	}

	protected virtual void DrawButton()
	{
		if(GUILayout.Button("Create WeaponUpgrade"))
		{
			CreateUpgrade<WeaponUpgrade>("Create WeaponUpgrade", "WeaponUpgrade", "Create a new WeaponUpgrade");
		}
	}

	protected void CreateUpgrade<T>(string title, string defaultName, string description) where T : WeaponUpgrade
	{
		string path = EditorUtility.SaveFilePanelInProject(title, defaultName, "asset", description);

		if(!string.IsNullOrEmpty(path))
		{
			T obj = CreateInstance<T>();

			AssetDatabase.CreateAsset(obj, path);
			AssetDatabase.SaveAssets();

			serializedObject.Update();

			if(prop_baseUpgrade.objectReferenceValue == null)
			{
				prop_baseUpgrade.objectReferenceValue = obj;
			}
			else
			{
				int index = prop_upgradeData.arraySize;

				prop_upgradeData.InsertArrayElementAtIndex(index);
				prop_upgradeData.GetArrayElementAtIndex(index).objectReferenceValue = obj;
			}

			serializedObject.ApplyModifiedProperties();

			Selection.activeObject = obj;
		}
	}
}