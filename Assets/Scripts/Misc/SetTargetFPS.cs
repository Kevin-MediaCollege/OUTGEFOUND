using UnityEngine;
using System.Collections;

public class SetTargetFPS : MonoBehaviour
{
	[SerializeField] private int targetFrameRate;

	protected void Awake()
	{
		Application.targetFrameRate = targetFrameRate;
	}
}