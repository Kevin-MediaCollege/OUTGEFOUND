using UnityEngine;
using System.Collections;

public class TaskMoveCloser : TaskBase
{
	public TaskMoveCloser()
	{
		
	}

	public virtual IEnumerator runTask(AIBase _ai)
	{
		while(true)
		{
			yield return new WaitForSeconds (0.5f);
		}
	}
}
