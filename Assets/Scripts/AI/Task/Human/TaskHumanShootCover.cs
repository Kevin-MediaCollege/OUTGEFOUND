using UnityEngine;
using System.Collections;

public class TaskHumanShootCover : TaskHuman
{
	public override IEnumerator RunTask(AIHuman _ai)
	{
		for(int i = 0; i < 16; i++)
		{
			yield return new WaitForSeconds (0.25f);

			_ai.transform.LookAt(EnemyUtils.PlayerCenter);
			Vector3 euler = _ai.transform.rotation.eulerAngles;
			euler.x = 0;
			_ai.transform.rotation = Quaternion.Euler(euler);

			_ai.Entity.Events.Invoke(new StartFireEvent());

			if(!_ai.CanSeePlayer() || !_ai.CurrentCover.IsSafe)
			{
				_ai.Entity.Events.Invoke(new StopFireEvent());
				break;
			}

			if(_ai.Entity.GetMagazine().Remaining == 0)
			{
				_ai.Entity.Events.Invoke(new ReloadEvent());
				break;
			}
		}
	}
}