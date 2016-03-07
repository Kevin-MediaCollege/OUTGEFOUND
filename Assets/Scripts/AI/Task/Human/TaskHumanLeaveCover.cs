using UnityEngine;
using System.Collections;

public class TaskHumanLeaveCover
{
	public IEnumerator runTask(AIHuman _human)
	{
		_human.currentCover = null;
		yield return new WaitForSeconds (0.5f);
	}
}