using UnityEngine;
using System.Collections.Generic;

public class FirearmBulletDecal : WeaponComponent
{
	[SerializeField] private GameObject decalPrefab;
	[SerializeField] private int maxDecals;

	private List<GameObject> decalPool = new List<GameObject>();

	private GameObject decalParent;
	private int next;

	protected override void Awake()
	{
		base.Awake();

		decalParent = new GameObject("Decals");

		for(int i = 0; i < maxDecals; i++)
		{
			GameObject decal = Instantiate(decalPrefab);

			decal.SetActive(false);
			decal.transform.SetParent(decalParent.transform);

			decalPool.Add(decal);
		}
	}

	protected override void OnFire(HitInfo hitInfo)
	{
		if(hitInfo.Tag == "Wall")
		{
			GameObject decal = GetDecalObject();
			decal.SetActive(true);

			decal.transform.position = hitInfo.Point + (hitInfo.Normal * 0.01f);
			decal.transform.rotation = Quaternion.FromToRotation(transform.forward, hitInfo.Normal) * transform.rotation;
		}
	}

	private GameObject GetDecalObject()
	{
		next++;
		if(next >= maxDecals)
		{
			next = 0;
		}

		return decalPool[next];
	}
}