using UnityEngine;
using System.Collections;

public class AIHuman : AIBase 
{
	void Awake()
	{
		StartCoroutine("run");
	}

	public override IEnumerator run()
	{
		Entity player = EntityUtils.GetEntityWithTag ("Player");

		while(true)
		{
			if(Vector3.Distance(player.gameObject.transform.position, gameObject.transform.position) < 20f)
			{
				//get closest cover;

				//if(any cover nearby?)
				//{
					//start task: walk towards cover

					//result: completed walk

					//if(line of sight?)
					//{
						//start task: shoot at player from cover

						//ends when any result (cover unsafe or lost line of sight)

						//(when unsafe why not just shoot instead of finding new cover?)
					//}
				//}
				//else if(line of sight?)
				//{
					//start task: shoot at player

					//ends when line of sight is lost

					//(why not end when need for reload?)
				//}
				//else
				//{
					//start task: walk towards player

					//ends with result of player being visible

					//(why not shoot at player instead?)
					//(why not end when need for reload?)
				//}
			}
			else
			{


				//start task: walk closer to player until x range

				//ends with result of player being close or visible
			}

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
}
