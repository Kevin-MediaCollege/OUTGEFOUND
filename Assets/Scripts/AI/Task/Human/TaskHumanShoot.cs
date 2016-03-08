using UnityEngine;
using System.Collections;

public class TaskHumanShoot
{
	public IEnumerator runTask(AIHuman _human)
	{
		for(int i = 0; i < 16; i++)
		{
			yield return new WaitForSeconds (0.25f);

			_human.transform.LookAt(EnemyUtils.PlayerCenter);
			Vector3 euler = _human.transform.rotation.eulerAngles;
			euler.x = 0;
			_human.transform.rotation = Quaternion.Euler(euler);

			_human.Entity.Events.Invoke(new StartFireEvent());

			if(!_human.canSeePlayer())
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