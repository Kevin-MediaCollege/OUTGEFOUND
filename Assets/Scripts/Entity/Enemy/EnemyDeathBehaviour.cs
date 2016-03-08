using UnityEngine;
using System.Collections;

public class EnemyDeathBehaviour : MonoBehaviour
{
	private Entity entity;

	protected void Awake()
	{
		entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<EntityDiedEvent>(OnDead);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<EntityDiedEvent>(OnDead);
	}

	private void OnDead(EntityDiedEvent evt)
	{
		Destroy(entity.gameObject);
	}
}
