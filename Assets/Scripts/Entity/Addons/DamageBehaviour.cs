using UnityEngine;
using System.Collections;

public class DamageBehaviour : EntityAddon
{
	[SerializeField] private AudioAssetGroup damageSounds;
	[SerializeField] private float delay = 2;

	private AudioManager audioManager;
	private float lastTime;

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
		if(Time.time - lastTime >= delay)
		{
			lastTime = Time.time;
			audioManager.PlayRandomAt(damageSounds, Entity.transform.position);
		}		
	}
}