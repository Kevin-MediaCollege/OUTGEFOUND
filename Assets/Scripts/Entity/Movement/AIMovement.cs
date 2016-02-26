using UnityEngine;
using System.Collections;

public class AIMovement : EntityMovement
{
	[SerializeField] private NavMeshAgent navMeshAgent;

	public bool MoveTo(Vector3 point)
	{
		if(navMeshAgent.SetDestination(point))
		{
			navMeshAgent.speed = speed;
			return true;
		}

		return false;
	}
}