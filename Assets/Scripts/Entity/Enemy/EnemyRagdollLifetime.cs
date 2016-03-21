using UnityEngine;
using System.Collections;

public class EnemyRagdollLifetime : MonoBehaviour
{
	[SerializeField] private float lifetime;

	protected void Start()
	{
		StartCoroutine("Destroy");
	}

	private IEnumerator Destroy()
	{
		yield return new WaitForSeconds(lifetime);

		Destroy(gameObject);
	}
}