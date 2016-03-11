using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private HashSet<IGameDependency> dependencies;

	protected void OnEnable()
	{
		dependencies = new HashSet<IGameDependency>();

		IEnumerable<Type> types = Reflection.AllTypesFrom<IGameDependency>();
		foreach(Type type in types)
		{
			IGameDependency dependency = Activator.CreateInstance(type) as IGameDependency;

			dependencies.Add(dependency);
			dependency.Start();
		}

		GlobalEvents.Invoke(new GameStartedEvent());
	}

	protected void OnDisable()
	{
		GlobalEvents.Invoke(new GameStoppedEvent());

		foreach(IGameDependency dependency in dependencies)
		{
			dependency.Stop();
		}
	}
}