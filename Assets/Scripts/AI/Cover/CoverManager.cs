using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoverManager : IGameDependency 
{
	private List<CoverBase> all;

	private CoroutineRunner coroutineRunner;

	private int nextCover = 0;
	private int updatesPerFrame = 5;

	private bool updatingEnabled = true;

	public CoverManager() 
	{
		all = new List<CoverBase>();

		coroutineRunner = Dependency.Get<CoroutineRunner>();
	}

	public void Start()
	{
		coroutineRunner.StartCoroutine(Update());
	}

	public void Stop()
	{
		coroutineRunner.StopCoroutine(Update());
	}

	private IEnumerator Update()
	{
		while(true)
		{
			if(updatingEnabled)
			{

				nextCover = nextCover >= all.Count ? 0 : nextCover;
				int end = nextCover + updatesPerFrame;
				end = end >= all.Count ? all.Count : end;
				Vector3 playerFeet = EnemyUtils.Player.transform.position;
				Vector3 playerHead = EnemyUtils.Player.GetEyes().position;

				for(int i = nextCover; i < end; i++)
				{
					all[i].UpdateCover(playerFeet, playerHead);
					//Debug.Log ("safe=" + all[i].IsSafe + " usefull=" + all[i].IsUsefull);

					nextCover++;
				}

				int c = 0;
				int c2 = 0;

				foreach(CoverBase cover in all)
				{
					if(cover.IsUsefull)
					{
						c++;

						if(cover.IsSafe)
						{
							c2++;
						}
					}
				}
			}
			yield return null;
		}
	}

	public void AddCover(CoverBase _cover)
	{
		all.Add(_cover);
	}

	public CoverBase GetNearestCover(Vector3 _current, Vector3 _player)
	{
		CoverBase cover = null;
		float closest = Vector3.Distance(_current, _player);

		foreach(CoverBase c in all)
		{
			if (c.IsSafe && c.IsUsefull && !c.IsOccupied) 
			{
				float dist = Vector3.Distance (_current, c.transform.position);
				if(dist < closest)
				{
					closest = dist;
					cover = c;
				}
			}
		}

		return cover;
	}
}