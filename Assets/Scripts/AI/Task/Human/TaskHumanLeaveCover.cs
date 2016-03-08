using UnityEngine;
using System.Collections;

public class TaskHumanLeaveCover : TaskHuman
{
	public override IEnumerator RunTask(AIHuman _ai)
	{
		_ai.CurrentCover = null;

		yield return new WaitForSeconds(0.5f);
	}
}