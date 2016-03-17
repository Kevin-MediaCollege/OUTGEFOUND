using UnityEngine;
using System.Collections;

public class DamageBehaviour : EntityAddon
{
	[SerializeField] private AudioAssetGroup damageSounds;

	private AudioManager audioManager;

	protected override void Awake()
	{
		base.Awake();

		audioManager = Dependency.Get<AudioManager>();
	}

	protected void OnEnable()
	{
		Entity.Events.AddListener<WeaponDamageEvent>(OnDamageReceived);
	}

	protected void OnDisable()
	{
		Entity.Events.AddListener<WeaponDamageEvent>(OnDamageReceived);
	}

	private void OnDamageReceived(WeaponDamageEvent evt)
	{
		audioManager.PlayRandomAt(damageSounds, Entity.transform.position);
	}
}