using UnityEngine;
using System.Collections;

public class TaskHumanGuard
{
	public IEnumerator runTask(AIHuman _human)
	{
		yield return new WaitForSeconds (1f);
	}
}