using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoverManager : MonoBehaviour 
{
	public static CoverManager instance;
	private List<CoverBase> coverList;
	private int coverListLength;
	private bool coverUpdated;

	public Transform a;
	public Transform b;
	public Transform c;
	public NavMeshAgent agentA;
	public NavMeshAgent agentB;

	void Awake () 
	{
		instance = this;
		coverList = new List<CoverBase>();
	}

	void Update()
	{
		coverUpdated = false;

		CoverBase cb = getClosestCover (b.position);
		if(cb != null)
		{
			c.transform.position = cb.gameObject.transform.position;
			agentA.SetDestination (b.transform.position);
			agentB.SetDestination (c.transform.position);
		}
	}

	public void addCover(CoverBase _cover)
	{
		coverList.Add (_cover);
		coverListLength++;
	}

	public CoverBase getClosestCover(Vector3 _current)
	{
		CoverBase cover = null;
		float closest = 999999f;

		Vector3 playerPos = a.position;//EntityUtils.GetEntityWithTag ("Player").gameObject.transform.position;
		for (int i = 0; i < coverListLength; i++)
		{
			if ((!coverUpdated && isCoverSafe (coverList[i], playerPos)) || (coverUpdated && coverList[i].isSafe)) 
			{
				float dist = Vector3.Distance (_current, coverList [i].gameObject.transform.position);
				if(dist < closest)
				{
					closest = dist;
					cover = coverList [i];
				}
			}
		}

		return cover;
	}

	public bool isCoverSafe(CoverBase _cover, Vector3 _playerPos)
	{
		bool result = Mathf.Abs ((_cover.getAngle () - RotationHelper.fixRotation (_cover.getAngle (), 
			RotationHelper.rotationToPoint (_cover.gameObject.transform.position, _playerPos)))) < _cover.coverAngle ? true : false;
		
		_cover.isSafe = result;
		return result;
	}
}
