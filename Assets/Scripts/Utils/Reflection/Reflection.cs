using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class Reflection
{
	public static IEnumerable<Type> AllTypesFrom<T>(bool checkInterfaces = false)
	{
		return AllTypesFrom(typeof(T), checkInterfaces);
	}

	public static IEnumerable<Type> AllTypesFrom(Type type, bool checkInterfaces = false)
	{
		Assembly projAssembly = Assembly.GetAssembly(typeof(Reflection));

		return projAssembly.GetTypes().Where(t =>
		{
			if(t.IsInterface || t.IsAbstract || t == type)
			{
				return false;
			}

			if(checkInterfaces)
			{
				foreach(Type interfaceType in t.GetInterfaces())
				{
					if(type.IsAssignableFrom(interfaceType))
					{
						return true;
					}
				}
			}

			return type.IsAssignableFrom(t);
		});
	}

	public static IEnumerable<string> AllTypeStringsFrom<T>(bool checkInterfaces = false)
	{
		return AllTypeStringsFrom(typeof(T), checkInterfaces);
	}

	public static IEnumerable<string> AllTypeStringsFrom(Type type, bool checkInterfaces = false)
	{
		IEnumerable<Type> types = AllTypesFrom(type, checkInterfaces);
		List<string> result = new List<string>();

		foreach(Type t in types)
		{
			result.Add(t.FullName);
		}

		return result;
	}
}