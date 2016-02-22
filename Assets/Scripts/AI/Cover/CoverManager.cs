using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoverManager : MonoBehaviour 
{
	public static CoverManager instance;
	public List<CoverBase> coverList;

	public Transform a;

	public CoverBase coverTest;

	void Awake () 
	{
		instance = this;
	}

	void Update()
	{
		Debug.Log ("cover=" + isCoverSafe (coverTest, a.position));
	}

	public void addCover(CoverBase _cover)
	{
		coverList.Add (_cover);
	}

	public void getClosestCover()
	{
		Vector3 playerPos = EntityUtils.GetEntityWithTag ("Player").gameObject.transform.position;
	}

	public bool isCoverSafe(CoverBase _cover, Vector3 _playerPos)
	{
		float r = RotationHelper.rotationToPoint (_cover.gameObject.transform.position, _playerPos);
		return Mathf.Abs((_cover.getAngle() - RotationHelper.fixRotation(_cover.getAngle(), r))) < 60f ? true : false;
	}
}
