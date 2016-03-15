using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour 
{
	public LayerMask layer;
	public GameObject enemyObject;
	public GameObject enemyParent;

	public EnemySpawnPoint[] points;
	private int pointsLenght;
	private int nextSpawnCheck;

	private Entity e;

	public float testDelay = 8f;

	void Awake () 
	{
		pointsLenght = points.Length;
		nextSpawnCheck = 0;
		enemyObject.SetActive (false);
	}

	void Update()
	{
		//==================== SPAWN CHECK ====================
		if(e == null)
		{
			e = EntityUtils.GetEntityWithTag ("Player");
		}

		if(nextSpawnCheck >= pointsLenght)
		{
			nextSpawnCheck = 0;
		}

		points [nextSpawnCheck].updateSpawn (layer, e.gameObject.transform.position);
		nextSpawnCheck++;
		//=====================================================

		testDelay -= Time.deltaTime;
		if(testDelay < 0f)
		{
			EnemySpawnPoint s = getRandomSpawnpoint();
			if(s != null)
			{
				GameObject g = GameObject.Instantiate (enemyObject);
				g.transform.SetParent (enemyParent.transform);
				g.transform.position = s.transform.position + new Vector3 (0f, 1f, 0f);
				g.SetActive (true);
			}
			testDelay = 6f;
		}
	}

	public EnemySpawnPoint getRandomSpawnpoint()
	{
		//ik remove dit als er geen errors gebeuren
		int breaksafe = 0;

		if(pointsLenght == 0)
		{
			Debug.LogError("Spawnpoint array lenght is 0");
			return null;
		}

		int start = Random.Range(0, pointsLenght - 1);
		for(int i = start + 1; i != start; i++)
		{
			if(i == pointsLenght)
			{
				i = 0;
			}

			if(points[i].safe)
			{
				return points[i];
			}

			//ik remove dit als er geen errors gebeuren
			breaksafe++;
			if(breaksafe > 100)
			{
				Debug.LogWarning("Failed to find a safe spawnpoint");
				break;
			}
		}

		return null;
	}
}
