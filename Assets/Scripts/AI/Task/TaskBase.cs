using UnityEngine;
using System.Collections;

public class TaskBase
{
	public virtual IEnumerator runTask(AIBase _ai)
	{
		yield return new WaitForSeconds (1f);
	}
}
