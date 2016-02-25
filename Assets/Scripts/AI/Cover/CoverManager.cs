using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoverManager : MonoBehaviour 
{
	public static CoverManager instance;
	private List<CoverBase> coverList;
	private int coverListLength;

	private int nextCover = 0;
	private int updatesPerFrame = 20;
	private bool updatingEnabled = true;

	void Awake () 
	{
		instance = this;
		coverList = new List<CoverBase>();
	}

	void Update()
	{
		if(updatingEnabled)
		{
			nextCover = nextCover >= coverListLength ? 0 : nextCover;
			int end = nextCover + updatesPerFrame;
			end = end >= coverListLength ? coverListLength : end;
			Vector3 playerPos = EntityUtils.GetEntityWithTag ("Player").gameObject.transform.position;

			for(int i = nextCover; i < end; i++)
			{
				updateCover (coverList[i], playerPos);
				nextCover++;
			}
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

		for (int i = 0; i < coverListLength; i++)
		{
			if (coverList[i].isSafe) 
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

	public void updateCover(CoverBase _cover, Vector3 _playerPos)
	{
		_cover.isSafe = Mathf.Abs ((_cover.getAngle () - RotationHelper.fixRotation (_cover.getAngle (), 
			RotationHelper.rotationToPoint (_cover.gameObject.transform.position, _playerPos)))) < _cover.coverAngle ? true : false;
	}
}
