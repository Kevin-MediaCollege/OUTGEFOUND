using System;
using System.Collections.Generic;

public class Dependency
{
	private static Dictionary<Type, IDependency> dependencies;

	static Dependency()
	{
		dependencies = new Dictionary<Type, IDependency>();
	}

	public static T Get<T>() where T : IDependency, new()
	{
		Type type = typeof(T);

		if(dependencies.ContainsKey(type))
		{
			return (T)dependencies[type];
		}

		IDependency dependency = Activator.CreateInstance<T>();
		dependencies.Add(type, dependency);

		return (T)dependency;
	}
}