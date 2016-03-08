using UnityEngine;

/// <summary>
/// AI movement controller
/// </summary>
public class AIMovement : EntityMovement
{
	public float RemainingDistance
	{
		get
		{
			return navMeshAgent.remainingDistance;
		}
	}

	[SerializeField] private NavMeshAgent navMeshAgent;

	public bool MoveTo(Vector3 point)
	{
		if(navMeshAgent.SetDestination(point))
		{
			navMeshAgent.speed = speed;
			navMeshAgent.Resume();

			return true;
		}

		Stop();
		return false;
	}

	public void Stop()
	{
		navMeshAgent.Stop();
	}
}