using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PathFindingEditor : MonoBehaviour
{
	public NodeBase[] nodeList;

	void Update () 
	{
	}

	private void updateNodes()
	{
		
	}

	void OnDrawGizmosSelected()
	{
		int c = 0, j = 0, l = nodeList.Length;
		for(int i = 0; i < nodeList.Length; i++)
		{
			if(nodeList[i] != null)
			{
				Gizmos.color = new Color(1f, 1f, 1f);
				c = nodeList[i].connections.Length;
				for(j = 0; j < c; j++)
				{
					if(nodeList[i].connections[j] != null)
					{
						Gizmos.DrawLine(nodeList[i].gameObject.transform.position, nodeList[i].connections[j].gameObject.transform.position);
					}
				}
				Gizmos.color = new Color(1f, 0f, 0f);
				Gizmos.DrawCube(nodeList[i].gameObject.transform.position, new Vector3(1f, 1f, 1f));
			}
		}
	}
}
