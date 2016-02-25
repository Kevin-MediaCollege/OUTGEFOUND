using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraStates : ScriptableObjectSingleton<CameraStates>
{
	[SerializeField] private GameObject[] states;
	[SerializeField] private GameObject defaultState;

	public static GameObject GetById(int id)
	{
		if(id >= Instance.states.Length || id < 0)
		{
			return Instance.defaultState;
		}

		return Instance.states[id];
	}

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Scriptable Objects/CameraStates")]
	private static void Create()
	{
		CreateAssetAt("Assets/Data/Resources/CameraStates.asset");
	}
#endif
}