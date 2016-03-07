using UnityEngine;
using System.Collections;

public class TaskHumanWalkToCover
{
	public IEnumerator runTask(AIHuman _human, Vector3 _target)
	{
		_human.movement.MoveTo(_target);

		while(true)
		{
			yield return new WaitForSeconds(0.25f);

			if(_human.movement.getMovingDistance() < 0.5f)
			{
				break;
			}
			if(!_human.currentCover.isSafe)
			{
				break;
			}
		}
	}
}