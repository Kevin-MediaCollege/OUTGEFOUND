using UnityEngine;
using System.Collections;

public class AIHuman : AIBase 
{
	public CoverBase CurrentCover { set; get; }

	private LastKnownPosition lastKnownPosition;
	private CoverManager coverManager;

	private float testDelay;

	private bool testShooting = false;

	protected override void Awake()
	{
		base.Awake();

		lastKnownPosition = Dependency.Get<LastKnownPosition>();
		coverManager = Dependency.Get<CoverManager>();
	}

	protected void OnEnable()
	{
		StartCoroutine("Run");
	}

	protected void OnDisable()
	{
		StopCoroutine("Run");
	}

	public override IEnumerator Run()
	{
		while(true)
		{
			testShooting = false;
			Vector3 playerPosition = EnemyUtils.Player.transform.position;
			
			if(!lastKnownPosition.Seen) //has player been seen recently?
			{
				if(CurrentCover != null) //is in cover?
				{
					//Debug.Log("NEW TASK: leave cover");
					yield return (new TaskHumanLeaveCover()).RunTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: guard");
					CheckRemoveCover();
					yield return (new TaskHumanGuard()).RunTask(this);	
				}
			}
			else if(CurrentCover != null) //is in cover?
			{
				if(CurrentCover.IsUsefull && CurrentCover.IsSafe) //is player visible from cover? && is cover safe?
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
			else if (CanSeePlayer()) //is player visible?
			{
				if((CurrentCover = coverManager.GetNearestCover(transform.position, playerPosition)) != null) //any empty cover nearby?
				{
					//Debug.Log("NEW TASK: Walk to cover");
					CurrentCover.Occupant = Entity;

					TaskHumanWalkToCover task = new TaskHumanWalkToCover(CurrentCover.transform.position);
					yield return task.RunTask(this);
				}
				else
				{
					//Debug.Log("NEW TASK: shoot at player");
					testShooting = true;
					CheckRemoveCover();
					yield return (new TaskHumanShoot()).RunTask(this);
				}
			}
			else if(Vector3.Distance(transform.position, playerPosition) < 2f) //is human close to last known position? TODO: or player can see last known position?
			{
				//Debug.Log("NEW TASK: guard");
				CheckRemoveCover();
				yield return (new TaskHumanGuard()).RunTask(this);
			}
			else
			{
				//Debug.Log("NEW TASK: track player");
				CheckRemoveCover();
				yield return (new TaskHumanTrack()).RunTask(this);
			}
			
			yield return null;
		}
	}

	public bool CanSeePlayer()
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

	public void CheckRemoveCover()
	{
		if(CurrentCover != null)
		{
			CurrentCover.Occupant = null;
			CurrentCover = null;
		}
	}

	protected void OnDrawGizmos()
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