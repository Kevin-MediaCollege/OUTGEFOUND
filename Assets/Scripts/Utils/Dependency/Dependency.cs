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
		return (T)Get(typeof(T));
	}

	public static IDependency Get(Type type)
	{
		if(dependencies.ContainsKey(type))
		{
			return dependencies[type];
		}

		IDependency dependency = Activator.CreateInstance(type) as IDependency;
		dependencies.Add(type, dependency);

		return dependency;
	}
}