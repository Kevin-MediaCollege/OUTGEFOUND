using System.Collections;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
	public EntityHealth health;

	void OnEnable()
	{
		health.onDamageTakenEvent += OnDamgeTakenEvent;
		health.onDeathEvent += OnDeathEvent;
	}

	void OnDisable()
	{
		health.onDamageTakenEvent -= OnDamgeTakenEvent;
		health.onDeathEvent -= OnDeathEvent;
	}

	private void OnDamgeTakenEvent(HitInfo hitInfo)
	{
		Debug.Log(hitInfo.target + " took " + hitInfo.damage + " damage", hitInfo.target);
	}

	private void OnDeathEvent(HitInfo hitInfo)
	{
		health.Entity.gameObject.SetActive(false);
	}
}