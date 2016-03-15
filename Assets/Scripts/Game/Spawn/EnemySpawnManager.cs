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
			GameObject g = GameObject.Instantiate (enemyObject);
			g.transform.SetParent (enemyParent.transform);
			g.transform.position = points [nextSpawnCheck].transform.position + new Vector3 (0f, 1f, 0f);
			g.SetActive (true);
			testDelay = 8f;
		}
	}

	public Vector3 getRandomSpawnpoint()
	{
		return Vector3.zero;
	}
}
