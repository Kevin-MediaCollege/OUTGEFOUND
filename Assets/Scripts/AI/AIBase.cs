using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour 
{
	public NavMeshAgent agent;

	public virtual void init()
	{
	}

	public virtual IEnumerator run()
	{
		yield break;
	}

	public virtual void destory()
	{
	}
}