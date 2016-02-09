using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public static class AudioMenuUtils
{
	[MenuItem("Audio/Create AudioAssets from selection")]
	private static void CreateAudioAssets()
	{
		if(Selection.objects.Length > 0)
		{
			foreach(Object obj in Selection.objects)
			{
				if(obj.GetType() == typeof(AudioClip))
				{
					string path = AssetDatabase.GetAssetPath(obj);

					if(!string.IsNullOrEmpty(path))
					{
						path = Path.ChangeExtension(path, ".asset");

						SerializedObject asset = new SerializedObject(CreateAudioAssetAtPath(path));

						asset.FindProperty("audioClip").objectReferenceValue = obj;
						asset.ApplyModifiedProperties();
						asset.Dispose();
					}
				}
			}

			AssetDatabase.SaveAssets();
		}
	}

	[MenuItem("Audio/Create AudioAssetGroup from selection")]
	private static void CreateAudioAssetGroups()
	{
		if(Selection.objects.Length > 0)
		{
			List<AudioAsset> targets = new List<AudioAsset>();

			foreach(Object obj in Selection.objects)
			{
				if(obj.GetType() == typeof(AudioAsset))
				{
					targets.Add(obj as AudioAsset);
				}
			}

			if(targets.Count > 0)
			{
				string path = AssetDatabase.GetAssetPath(targets[0]);

				if(!string.IsNullOrEmpty(path))
				{
					int lastIndex = path.LastIndexOf(Path.DirectorySeparatorChar);

					if(lastIndex <= 0)
					{
						lastIndex = path.LastIndexOf(Path.AltDirectorySeparatorChar);
					}

					if(lastIndex > 0)
					{
						path = path.Substring(0, lastIndex + 1);
						path += "New AudioAssetGroup.asset";
					}
					else
					{
						path = EditorUtility.SaveFilePanelInProject("Create AudioAssetGroup", "New AudioAssetGroup", "asset", "Create a new Audio Asset Group");

						if(string.IsNullOrEmpty(path))
						{
							return;
						}
					}

					CreateAudioAssetGroupAtPath(path, targets);
				}
			}
		}
	}

	private static AudioAsset CreateAudioAssetAtPath(string path)
	{
		AudioAsset asset = ScriptableObject.CreateInstance<AudioAsset>();

		AssetDatabase.CreateAsset(asset, path);
		AssetDatabase.SaveAssets();

		return asset;
	}

	private static AudioAssetGroup CreateAudioAssetGroupAtPath(string path, List<AudioAsset> audioAssets = null)
	{
		AudioAssetGroup asset = ScriptableObject.CreateInstance<AudioAssetGroup>();

		AssetDatabase.CreateAsset(asset, path);
		AssetDatabase.SaveAssets();

		if(audioAssets != null && audioAssets.Count > 0)
		{
			SerializedObject so = new SerializedObject(asset);
			SerializedProperty element = so.FindProperty("audioAssets");

			for(int i = 0; i < audioAssets.Count; i++)
			{
				element.InsertArrayElementAtIndex(i);
				element.GetArrayElementAtIndex(i).objectReferenceValue = audioAssets[i];
			}

			so.ApplyModifiedProperties();
		}

		Selection.activeObject = asset;
		return asset;
	}
}