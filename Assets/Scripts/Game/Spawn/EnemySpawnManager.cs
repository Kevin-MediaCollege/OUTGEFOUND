using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour 
{
	public LayerMask layer;
	public string enemyObject;
	public GameObject enemyParent;

	public EnemySpawnPoint[] points;
	private int pointsLenght;
	private int nextSpawnCheck;

	private Entity e;

	public float startDelay = 8f;
	private float delay = 8f;

	void Awake () 
	{
		pointsLenght = points.Length;
		nextSpawnCheck = 0;
		delay = 1f;
		if(startDelay < 3f) { startDelay = 3f; }
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

		delay -= Time.deltaTime;
		if(delay < 0f)
		{
			EnemySpawnPoint s = getRandomSpawnpoint();
			if(s != null)
			{
				GameObject g = Instantiate(Resources.Load(enemyObject)) as GameObject;
				g.transform.SetParent (enemyParent.transform);
				g.transform.position = s.transform.position + new Vector3 (0f, 1f, 0f);
				g.SetActive (true);
			}

			if(startDelay > 6f)
			{
				startDelay -= 0.1f;
				//Debug.Log ("" + startDelay);
			}
			else if(startDelay > 4.5f)
			{
				startDelay -= 0.03f;
				//Debug.Log ("" + startDelay);
			}
			else if(startDelay > 3.5f)
			{
				startDelay -= 0.01f;
				//Debug.Log ("" + startDelay);
			}
			delay = startDelay;
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
				//Debug.LogWarning("Failed to find a safe spawnpoint");
				break;
			}
		}

		return null;
	}
}
