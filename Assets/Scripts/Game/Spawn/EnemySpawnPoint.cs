using UnityEngine;
using System.Collections;

public class EnemySpawnPoint : MonoBehaviour 
{
	private static Vector3[] directions = new Vector3[]{
		new Vector3(0f, 2f, 0f), new Vector3(2f, 2f, 0f), new Vector3(-2f, 2f, 0f), new Vector3(0f, 2f, 2f), new Vector3(0f, 2f, -2f),
	};

	public bool safe;

	void Awake ()
	{
		safe = false;
	}

	public void updateSpawn(LayerMask _layer, Vector3 _playerPos)
	{
		safe = true;
		Vector3 pos = gameObject.transform.position;
		float dist = Vector3.Distance (_playerPos, pos);

		if(dist < 15f)
		{
			safe = false;
			return;
		}

		RaycastHit[] hits = Physics.RaycastAll(pos + new Vector3(0f, 1f, 0f), (_playerPos + new Vector3(0f, 0.5f, 0f)) - (pos + new Vector3(0f, 1f, 0f)), dist);
		int l = hits.Length;
		int c = 0;
		for(int i = 0; i < l; i++)
		{
			if (hits [i].collider.gameObject.CompareTag ("Wall")) 
			{
				c++;
			}
		}

		if(c == 0)
		{
			safe = false;
			return;
		}

		for(int j = 0; j < 5; j++)
		{
			hits = Physics.RaycastAll(pos, directions[j], 2f, _layer.value);
			if(hits.Length > 0)
			{
				safe = false;
				return;
			}
		}
	}
}
