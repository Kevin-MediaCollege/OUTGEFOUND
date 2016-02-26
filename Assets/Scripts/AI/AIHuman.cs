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

			}
			else
			{

			}
		}
	}
}
