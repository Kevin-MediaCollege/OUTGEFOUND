using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Firearm))]
public class FirearmEditor : WeaponEditor
{
	protected override void DrawButton()
	{
		if(GUILayout.Button("Create FirearmUpgrade"))
		{
			CreateUpgrade<FirearmUpgrade>("Create FirearmUpgrade", "FirearmUpgrade", "Create a new FirearmUpgrade");
		}
	}
}