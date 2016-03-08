using UnityEngine;
using System.Collections;

public class TaskHumanWalkToCover : TaskHuman
{
	private Vector3 target;

	public TaskHumanWalkToCover(Vector3 target)
	{
		this.target = target;
	}

	public override IEnumerator RunTask(AIHuman _ai)
	{
		_ai.MoveTo(target);

		while(true)
		{
			yield return new WaitForSeconds(0.25f);

			if(_ai.Movement.RemainingDistance < 0.5f)
			{
				break;
			}

			if(!_ai.CurrentCover.IsSafe)
			{
				break;
			}
		}
	}
}