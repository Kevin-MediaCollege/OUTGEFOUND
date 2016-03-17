using UnityEngine;
using System.Collections;

public class TaskHumanShootCover : TaskHuman
{
	private CoverBase cover;

	public TaskHumanShootCover(CoverBase _cover)
	{
		cover = _cover;
	}

	public override IEnumerator RunTask(AIHuman _ai)
	{
		if (cover.GetType () == typeof(CoverWall)) 
		{
			CoverWall coverObstacle = (CoverWall)cover;
			if (_ai.Entity.GetMagazine ().Remaining == 0) 
			{
				_ai.MoveTo (coverObstacle.transform.position);
				_ai.Entity.Events.Invoke (new AttemptReloadEvent ());
				yield return new WaitForSeconds (1f);
				yield break;
			}
			else 
			{
				_ai.MoveTo(coverObstacle.ShootPosition);
				for(int i = 0; i < 16; i++)
				{
					yield return new WaitForSeconds (0.1f);

					if(!_ai.IsMoving())
					{
						break;
					}
				}

				_ai.transform.LookAt(EnemyUtils.PlayerCenter);
				Vector3 euler = _ai.transform.rotation.eulerAngles;
				euler.x = 0;
				_ai.transform.rotation = Quaternion.Euler(euler);
				_ai.Entity.Events.Invoke(new StartFireEvent());
				EntityHealth health = _ai.Entity.GetHealth ();
				float startHealth = health.CurrentHealth;

				for(int i = 0; i < 20; i++)
				{
					yield return new WaitForSeconds (0.1f);

					if(!_ai.CanSeePlayer() || !_ai.CurrentCover.IsSafe)
					{
						_ai.Entity.Events.Invoke(new StopFireEvent());
						break;
					}

					if(_ai.Entity.GetMagazine().Remaining == 0)
					{
						_ai.MoveTo (coverObstacle.transform.position);
						for(int j = 0; j < 16; j++)
						{
							yield return new WaitForSeconds (0.1f);

							if(!_ai.IsMoving())
							{
								break;
							}
						}
						yield break;
					}

					if(health.CurrentHealth < startHealth - 2f)
					{
						_ai.MoveTo (coverObstacle.transform.position);
						for(int j = 0; j < 16; j++)
						{
							yield return new WaitForSeconds (0.1f);

							if(!_ai.IsMoving())
							{
								break;
							}

							yield return new WaitForSeconds (1f);
						}
						yield break;
					}
				}
			}
		} 
		else 
		{
			_ai.transform.LookAt(EnemyUtils.PlayerCenter);
			Vector3 euler = _ai.transform.rotation.eulerAngles;
			euler.x = 0;
			_ai.transform.rotation = Quaternion.Euler(euler);
			_ai.Entity.Events.Invoke(new StartFireEvent());

			for(int i = 0; i < 16; i++)
			{
				yield return new WaitForSeconds (0.25f);

				if(!_ai.CanSeePlayer() || !_ai.CurrentCover.IsSafe)
				{
					_ai.Entity.Events.Invoke(new StopFireEvent());
					break;
				}

				if(_ai.Entity.GetMagazine().Remaining == 0)
				{
					_ai.Entity.Events.Invoke(new AttemptReloadEvent());
					break;
				}
			}
		}
	}
}