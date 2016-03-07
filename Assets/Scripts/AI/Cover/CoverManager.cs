using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoverManager : MonoBehaviour 
{
	public static CoverManager instance;

	//List
	private List<CoverBase> coverList;
	private int coverListLength;

	//Update
	private int nextCover = 0;
	private int updatesPerFrame = 5;
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
			Vector3 playerFeet = EnemyUtils.Player.transform.position;
			Vector3 playerHead = EnemyUtils.Player.GetEyes().position;

			for(int i = nextCover; i < end; i++)
			{
				updateCover (coverList[i], playerFeet, playerHead);
				nextCover++;
			}

			int c = 0;
			int c2 = 0;
			for(int j = 0; j < coverListLength; j++)
			{
				if(coverList[j].isUsefull)
				{
					c++;
					if(coverList[j].isSafe)
					{
						c2++;
					}
				}
			}
		}
	}

	public void addCover(CoverBase _cover)
	{
		coverList.Add (_cover);
		coverListLength++;
	}

	public CoverBase getClosestCover(Vector3 _current, Vector3 _player)
	{
		CoverBase cover = null;
		float closest = Vector3.Distance(_current, _player);

		for (int i = 0; i < coverListLength; i++)
		{
			if (coverList[i].isSafe && coverList[i].isUsefull && !coverList[i].occupied) 
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

	public void updateCover(CoverBase _cover, Vector3 _playerPos, Vector3 _playerHead)
	{
		_cover.isUsefull = true;
		Vector3 start = _cover.transform.position + new Vector3(0f, _cover.coverSizeY + 0.5f, 0f);
		float dist = Vector3.Distance(start, _playerHead);

		if(dist < 5f)
		{
			_cover.isSafe = false;
			_cover.isUsefull = false;
			return;
		}

		_cover.isSafe = Mathf.Abs ((_cover.getAngle () - RotationHelper.fixRotation (_cover.getAngle (), 
			RotationHelper.rotationToPoint (_cover.gameObject.transform.position, _playerPos)))) < _cover.coverAngle ? true : false;

		if(!_cover.isSafe)
		{
			_cover.isUsefull = false;
			return;
		}

		_cover.isUsefull = true;
		RaycastHit[] hits = Physics.RaycastAll(start, _playerHead - start, dist);
		int l = hits.Length;
		for(int i = 0; i < l; i++)
		{
			if(hits[i].collider.gameObject.CompareTag("Wall"))
			{
				_cover.isUsefull = false;
				break;
			}
		}
	}

	/*
	void OnDrawGizmosSelected()
	{
		for(int j = 0; j < coverListLength; j++)
		{
			if(coverList[j].isSafe && coverList[j].isUsefull)
			{
				Gizmos.color = new Color(0f, 1f, 0f);
				Gizmos.DrawLine(coverList[j].transform.position + new Vector3(0f, coverList[j].coverSizeY + 0.5f, 0f), temp_playerHeadPosition.transform.position);
				Gizmos.DrawCube(coverList[j].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
			}
		}
	}
	*/
}