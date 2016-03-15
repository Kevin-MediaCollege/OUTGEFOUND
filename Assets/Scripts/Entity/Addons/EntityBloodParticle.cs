using UnityEngine;

/// <summary>
/// Spawn blood particles if the parent entity received damage
/// </summary>
public class EntityBloodParticle : EntityAddon
{
	[SerializeField] private GameObject bloodParticlePrefab;

	protected void OnEnable()
	{
		Entity.Events.AddListener<WeaponDamageEvent>(OnDamageEvent);
	}

	protected void OnDisable()
	{
		Entity.Events.RemoveListener<WeaponDamageEvent>(OnDamageEvent);
	}

	private void OnDamageEvent(WeaponDamageEvent evt)
	{
		GameObject bloodParticle = Instantiate(bloodParticlePrefab);
		bloodParticle.transform.SetParent(transform);

		bloodParticle.transform.position = evt.Damage.hit.point;
		bloodParticle.transform.rotation = Quaternion.FromToRotation(transform.forward, evt.Damage.hit.normal) * transform.rotation;
	}
}
