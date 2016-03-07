using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecalManager : IDependency
{
	private struct Decal
	{
		public Vector3 position;
		public Vector3 normal;

		public float time;
	}

	private List<Decal> decals;

	private CoroutineRunner coroutineRunner;

	public DecalManager()
	{
		decals = new List<Decal>();

		coroutineRunner = Dependency.Get<CoroutineRunner>();
	}

	public void Create()
	{
		coroutineRunner.StartCoroutine(UpdateDecals());
	}

	public void Destroy()
	{
		coroutineRunner.StopCoroutine(UpdateDecals());

		decals.Clear();
	}
	
	public void AddDecal(Vector3 position, Vector3 normal)
	{
		Decal decal = new Decal();
		decal.position = position;
		decal.normal = normal;
		decal.time = Time.time;

		decals.Add(decal);
	}

	private IEnumerator UpdateDecals()
	{
		HashSet<Decal> toRemove = new HashSet<Decal>();

		while(true)
		{
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
			yield return null;
		}
	}
}
