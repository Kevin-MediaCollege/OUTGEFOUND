using UnityEngine;
using System.Collections;

public class AIHuman : AIBase 
{
	[HideInInspector]
	public CoverBase currentCover;

	private LastKnownPosition lastKnownPosition;

	private float testDelay;

	private bool testShooting = false;

	protected override void Awake()
	{
		base.Awake();

		lastKnownPosition = Dependency.Get<LastKnownPosition>();
	}

	protected void Start()
	{
		StartCoroutine("Run");
	}

	public override IEnumerator Run()
	{
		while(true)
		{
			testShooting = false;
			Vector3 playerPosition = EnemyUtils.Player.transform.position;
			
			if(!lastKnownPosition.Seen) //has player been seen recently?
			{
				if(currentCover != null) //is in cover?
				{
					//Debug.Log("NEW TASK: leave cover");
					yield return (new TaskHumanLeaveCover()).RunTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: guard");
					checkRemoveCover();
					yield return (new TaskHumanGuard()).RunTask(this);	
				}
			}
			else if(currentCover != null) //is in cover?
			{
				if(currentCover.isUsefull && currentCover.isSafe) //is player visible from cover? && is cover safe?
				{
					//Debug.Log("NEW TASK: shoot at player from cover");
					testShooting = true;
					yield return (new TaskHumanShootCover()).RunTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: leave cover");
					yield return (new TaskHumanLeaveCover()).RunTask(this);
				}
			}
			else if (canSeePlayer()) //is player visible?
			{
				if((currentCover = CoverManager.instance.getClosestCover(gameObject.transform.position, playerPosition)) != null) //any empty cover nearby?
				{
					//Debug.Log("NEW TASK: Walk to cover");
					currentCover.occupied = true;

					TaskHumanWalkToCover task = new TaskHumanWalkToCover(currentCover.transform.position);
					yield return task.RunTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: shoot at player");
					testShooting = true;
					checkRemoveCover();
					yield return (new TaskHumanShoot()).RunTask(this);
				}
			}
			else if(Vector3.Distance(gameObject.transform.position, playerPosition) < 2f) //is human close to last known position? TODO: or player can see last known position?
			{
				//Debug.Log("NEW TASK: guard");
				checkRemoveCover();
				yield return (new TaskHumanGuard()).RunTask(this);
			}
			else
			{
				//Debug.Log("NEW TASK: track player");
				checkRemoveCover();
				yield return (new TaskHumanTrack()).RunTask(this);
			}

			//while safety
			yield return null;
		}
	}

	public bool canSeePlayer()
	{
		Vector3 eyePosition = Entity.GetEyes().position;
		Vector3 playerPosition = EnemyUtils.PlayerCenter;
		Vector3 direction = (playerPosition - eyePosition).normalized;
		
		RaycastHit[] hits = Physics.RaycastAll(eyePosition, direction, Vector3.Distance(eyePosition, playerPosition));
		
		foreach(RaycastHit hit in hits)
		{
			if(hit.collider.CompareTag("Wall"))
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
				Gizmos.DrawLine(Entity.GetEyes().position, EnemyUtils.PlayerCenter);
				testDelay = 0.35f;
			}
		}
	}
}