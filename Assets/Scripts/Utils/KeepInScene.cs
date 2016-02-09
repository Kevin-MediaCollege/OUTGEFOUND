using UnityEngine;
using System.Collections;

public class KeepInScene : MonoBehaviour
{
	protected void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}