using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
{
	private static T instance;
	protected static T Instance
	{
		get
		{
			if(instance == null)
			{
				instance = Load();
			}

			return instance;
		}
	}

#if UNITY_EDITOR
	protected static void CreateAsset(string title, string defaultName, string message)
	{
		string path = EditorUtility.SaveFilePanelInProject(title, defaultName, "asset", message);

		if(!string.IsNullOrEmpty(path))
		{
			CreateAssetAt(path);
		}
	}

	protected static void CreateAssetAt(string path)
	{
		T obj = CreateInstance<T>();

		AssetDatabase.CreateAsset(obj, path);
		AssetDatabase.SaveAssets();

		Selection.activeObject = obj;
	}
#endif

	private static T Load()
	{
		string[] elements = typeof(T).ToString().Split('.');
		string typeName = elements[elements.Length - 1];

		T asset = Resources.Load(typeName, typeof(T)) as T;

		if(asset == null)
		{
			T[] allObjects = Resources.FindObjectsOfTypeAll<T>();

			if(allObjects.Length == 0)
			{
				throw new Exception("Unable to find resource of type: " + typeof(T));
			}

			asset = allObjects[0];
		}

		return asset;
	}
}