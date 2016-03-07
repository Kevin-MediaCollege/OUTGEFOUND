using UnityEngine;
using System.Collections;

public class TaskHumanTrack
{
	public IEnumerator runTask(AIHuman _human)
	{
		Vector3 last = LastKnownPosition.instance.getPosition();
		_human.movement.MoveTo(last);

		while(true)
		{
			for(int i = 0; i < 4; i++)
			{
				yield return new WaitForSeconds(0.25f);

				if(_human.canSeePlayer()) //player visible
				{
					_human.movement.stopMoving();
					yield break;
				}
				if(Vector3.Distance(_human.gameObject.transform.position, last) < 3f) //walk complete
				{
					_human.movement.stopMoving();
					yield break;
				}
			}

			if(LastKnownPosition.instance.getPosition() != last) //player position changed
			{
				_human.movement.stopMoving();
				break;
			}
		}
	}
}