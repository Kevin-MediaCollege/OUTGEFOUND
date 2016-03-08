using UnityEngine;
using System.Collections;

public class TaskHumanTrack : TaskHuman
{
	private LastKnownPosition lastKnowPosition;

	public TaskHumanTrack()
	{
		lastKnowPosition = Dependency.Get<LastKnownPosition>();
	}

	public override IEnumerator RunTask(AIHuman _ai)
	{
		Vector3 last = lastKnowPosition.Position;

		if(!_ai.MoveTo(last))
		{
			yield break;
		}

		while(true)
		{
			for(int i = 0; i < 4; i++)
			{
				yield return new WaitForSeconds(0.25f);

				if(_ai.CanSeePlayer()) //player visible
				{
					_ai.Stop();
					yield break;
				}
				if(Vector3.Distance(_ai.transform.position, lastKnowPosition.Position) < 3f) //walk complete
				{
					_ai.Stop();
					yield break;
				}
			}

			if(lastKnowPosition.Position != last) //player position changed
			{
				_ai.Stop();
				break;
			}
		}
	}
}