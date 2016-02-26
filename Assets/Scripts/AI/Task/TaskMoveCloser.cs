using UnityEngine;
using System.Collections;

public class TaskMoveCloser : TaskBase
{
	public virtual IEnumerator runTask()
	{
		yield return new WaitForSeconds (1f);
	}
}
