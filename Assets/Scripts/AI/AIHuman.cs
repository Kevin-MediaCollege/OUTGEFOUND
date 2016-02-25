using UnityEngine;
using System.Collections;

public class AIHuman : AIBase 
{
	public override void init()
	{
		StartCoroutine("run");
	}

	public override IEnumerator run()
	{
		while(true)
		{
			/*

			if(is player close?)
			{
				(just shoot if player is too close?)

				get closest cover;

				if(any cover nearby?)
				{
					start task: walk towards cover

					result: completed walk

					if(line of sight?)
					{
						start task: shoot at player from cover

						ends when any result (cover unsafe or lost line of sight)

						(when unsafe why not just shoot instead of finding new cover?)
					}
				}
				else if(line of sight?)
				{
					start task: shoot at player

					ends when line of sight is lost

					(why not end when need for reload?)
				}
				else
				{
					start task: walk towards player

					ends with result of player being visible

					(why not shoot at player instead?)
					(why not end when need for reload?)
				}
			}
			else
			{
				start task: walk closer to player until x range

				//ends with result of player being close or visible
			}

			 */
		}
	}

	public override void destory()
	{
		StopAllCoroutines();
	}
}
