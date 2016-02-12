using UnityEngine;
using System.Collections;

public class NodeBase : MonoBehaviour 
{
	public NodeBase[] connections;

	void OnDrawGizmosSelected()
	{
		int l = connections.Length;
		for(int i = 0; i < l; i++)
		{
			if(connections[i] != null)
			{
				Gizmos.DrawLine(gameObject.transform.position, connections[i].gameObject.transform.position);
			}
		}
	}
}
