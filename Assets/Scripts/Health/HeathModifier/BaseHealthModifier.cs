using UnityEngine;
using System.Collections;

public abstract class BaseHealthModifier : MonoBehaviour, IHealthModifier
{
	private EntityHealth health;

	protected void Awake()
	{
		health = GetComponentInParent<EntityHealth>();
	}

	protected void OnEnable()
	{
		health.AddHealthModifier(this);
	}

	protected void OnDisable()
	{
		health.AddHealthModifier(this);
	}
		
	public abstract HitInfo OnDamageReceived(HitInfo hitInfo);
}