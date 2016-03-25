using UnityEngine;
using System.Collections;

public abstract class SettingSetter<T> : MonoBehaviour
{
	[SerializeField] protected string key;

	public abstract void Set(T value);
}