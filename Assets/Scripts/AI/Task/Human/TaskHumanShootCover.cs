using UnityEngine;
using System.Collections;

public class TaskHumanShootCover
{
	public IEnumerator runTask(AIHuman _human)
	{
		for(int i = 0; i < 16; i++)
		{
			yield return new WaitForSeconds (0.25f);

			_human.transform.LookAt(EnemyUtils.PlayerCenter);
			_human.Entity.Events.Invoke(new StartFireEvent());

			if(!_human.canSeePlayer() || !_human.currentCover.isSafe)
			{
				_human.Entity.Events.Invoke(new StopFireEvent());
				break;
			}

			if(_human.Entity.GetMagazine().Remaining == 0)
			{
				_human.Entity.Events.Invoke(new ReloadEvent());
				break;
			}
		}
	}
}