using UnityEngine;
using System.Collections;

public class AIHuman : AIBase 
{
	[HideInInspector]
	public CoverBase currentCover;

	private bool testShooting = false;
	private float testDelay = 0f;

	void Start()
	{
		StartCoroutine("run");
	}

	public override IEnumerator run()
	{
		while(true)
		{
			testShooting = false;

			if(!LastKnownPosition.instance.hasBeenSeen()) //has player been seen recently?
			{
				if(currentCover != null) //is in cover?
				{
					//Debug.Log("NEW TASK: leave cover");
					yield return (new TaskHumanLeaveCover()).runTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: guard");
					checkRemoveCover();
					yield return (new TaskHumanGuard()).runTask(this);	
				}
			}
			else if(currentCover != null) //is in cover?
			{
				if(currentCover.isUsefull && currentCover.isSafe) //is player visible from cover? && is cover safe?
				{
					//Debug.Log("NEW TASK: shoot at player from cover");
					testShooting = true;
					yield return (new TaskHumanShootCover()).runTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: leave cover");
					yield return (new TaskHumanLeaveCover()).runTask(this);
				}
			}
			else if (canSeePlayer()) //is player visible?
			{
				if((currentCover = CoverManager.instance.getClosestCover(gameObject.transform.position, movement.getPlayerPosition())) != null) //any empty cover nearby?
				{
					//Debug.Log("NEW TASK: Walk to cover");
					currentCover.occupied = true;
					yield return (new TaskHumanWalkToCover()).runTask(this, currentCover.gameObject.transform.position);
				}
				else
				{
					//Debug.Log("NEW TASK: shoot at player");
					testShooting = true;
					checkRemoveCover();
					yield return (new TaskHumanShoot()).runTask(this);
				}
			}
			else if(Vector3.Distance(gameObject.transform.position, movement.getPlayerPosition()) < 2f) //is human close to last known position? TODO: or player can see last known position?
			{
				//Debug.Log("NEW TASK: guard");
				checkRemoveCover();
				yield return (new TaskHumanGuard()).runTask(this);
			}
			else
			{
				//Debug.Log("NEW TASK: track player");
				checkRemoveCover();
				yield return (new TaskHumanTrack()).runTask(this);
			}

			//while safety
			yield return null;
		}
	}

	public bool canSeePlayer()
	{
		float dist = Vector3.Distance(movement.getHeadPosition(), movement.getPlayerHeadPosition());
		RaycastHit[] hits = Physics.RaycastAll(movement.getHeadPosition(), movement.getPlayerHeadPosition() - movement.getHeadPosition(), dist);
		int l = hits.Length;
		for(int i = 0; i < l; i++)
		{
			if(hits[i].collider.gameObject.CompareTag("Wall"))
			{
				return false;
			}
		}
		return true;
	}

	public void checkRemoveCover()
	{
		if(currentCover != null)
		{
			currentCover.occupied = false;
			currentCover = null;
		}
	}

	void OnDrawGizmos()
	{
		if(testShooting)
		{
			testDelay -= Time.deltaTime;
			if(testDelay < 0f)
			{
				Gizmos.color = new Color(1f, 0f, 0f);
				Gizmos.DrawLine(movement.getHeadPosition(), movement.getPlayerHeadPosition());
				testDelay = 0.35f;
			}
		}
	}
}