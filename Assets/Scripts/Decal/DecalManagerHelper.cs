using System;
using System.Collections.Generic;
using UnityEngine;

public class DecalManagerHelper : MonoBehaviour
{
	public struct Decal
	{
		public Vector3 position;
		public Vector3 normal;
		public string tag;

		public float time;
	}

	private List<Decal> decals;

	protected void Awake()
	{
		decals = new List<Decal>();
	}

	protected void OnEnable()
	{
		GlobalEvents.AddListener<SpawnDecalEvent>(OnSpawnDecalEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<SpawnDecalEvent>(OnSpawnDecalEvent);
	}

	protected void Update()
	{
		HashSet<Decal> toRemove = new HashSet<Decal>();

		foreach(Decal decal in decals)
		{
			float duration = Time.time - decal.time;

			if(duration > 10)
			{
				toRemove.Add(decal);
			}
		}

		foreach(Decal decal in toRemove)
		{
			decals.Remove(decal);
		}

		toRemove.Clear();
	}

	private void OnSpawnDecalEvent(SpawnDecalEvent evt)
	{
		Decal decal = new Decal();
		decal.position = evt.Position;
		decal.normal = evt.Normal;
		decal.tag = evt.Tag;
		decal.time = Time.time;

		decals.Add(decal);
	}
}