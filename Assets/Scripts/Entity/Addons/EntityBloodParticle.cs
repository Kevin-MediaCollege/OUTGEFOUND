using UnityEngine;
using System.Collections;
using System;

public class EntityBloodParticle : BaseEntityAddon
{
	[SerializeField] private GameObject bloodParticlePrefab;

	protected void OnEnable()
	{
		Entity.Events.AddListener<DamageEvent>(OnDamageEvent);
	}

	protected void OnDisable()
	{
		Entity.Events.RemoveListener<DamageEvent>(OnDamageEvent);
	}

	private void OnDamageEvent(DamageEvent evt)
	{
		GameObject bloodParticle = Instantiate(bloodParticlePrefab);
		bloodParticle.transform.SetParent(transform);

		bloodParticle.transform.position = evt.DamageInfo.Hit.Point;
		bloodParticle.transform.rotation = Quaternion.FromToRotation(transform.forward, evt.DamageInfo.Hit.Normal) * transform.rotation;
	}
}
