using UnityEngine;
using System.Collections;

public class TaskHumanGuard : TaskHuman
{
	public override IEnumerator RunTask(AIHuman _ai)
	{
		yield return new WaitForSeconds(1f);
	}
}