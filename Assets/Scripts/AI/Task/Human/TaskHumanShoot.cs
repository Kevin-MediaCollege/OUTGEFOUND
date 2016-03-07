using UnityEngine;
using System.Collections;

public class TaskHumanShoot
{
	public IEnumerator runTask(AIHuman _human)
	{
		for(int i = 0; i < 16; i++)
		{
			yield return new WaitForSeconds (0.25f);

			_human.movement.shootAtPlayer();

			if(!_human.canSeePlayer())
			{
				break;
			}

			//if empty mag
		}
	}
}