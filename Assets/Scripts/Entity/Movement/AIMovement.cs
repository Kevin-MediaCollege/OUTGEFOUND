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
	
	public void shootAtPlayer()
	{
		gameObject.transform.LookAt(getPlayerPosition());
	}

	public void stopShooting()
	{
		
	}

	public void reloadWeapon()
	{
		
	}

	public int getBulletsLeft()
	{
		return 0;
	}

	public float getMovingDistance()
	{
		return navMeshAgent.remainingDistance;
	}

	public void stopMoving()
	{
		navMeshAgent.Stop();
	}

	public Vector3 getHeadPosition()
	{
		return Vector3.zero;//head.transform.position;
	}

	public Vector3 getPlayerHeadPosition()
	{
		return Vector3.zero;//playerHead.transform.position;
	}

	public Vector3 getPlayerPosition()
	{
		return Vector3.zero;//playerHead.transform.position;
	}
}