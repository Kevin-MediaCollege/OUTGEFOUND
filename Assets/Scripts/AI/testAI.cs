using UnityEngine;
using System.Collections;

public class testAI : MonoBehaviour
{
	public Transform a;
	public Transform b;
	public Transform c;
	public NavMeshAgent agentA;
	public NavMeshAgent agentB;

	void Start () 
	{

	}

	void Update () 
	{
		CoverBase cb = CoverManager.instance.getClosestCover (b.position);
		if(cb != null)
		{
			c.transform.position = cb.gameObject.transform.position;
			agentA.SetDestination (b.transform.position);
			agentB.SetDestination (c.transform.position);
		}
	}
}