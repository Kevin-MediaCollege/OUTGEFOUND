using UnityEngine;
using System.Collections;

/// <summary>
/// Listens for the death of it's parent entity, and does something when that happens
/// </summary>
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
