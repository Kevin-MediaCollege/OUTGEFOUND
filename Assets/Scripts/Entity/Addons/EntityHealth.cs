using UnityEngine;

/// <summary>
/// Give an entity health. This does *not* allow it to die, you need an EntityDead component for that.
/// EntityHealth just keeps track of the entities current health
/// </summary>
public class EntityHealth : EntityAddon
{
	public float StartingHealth
	{
		get
		{
			return startingHealth;
		}
	}

	public float CurrentHealth { set; get; }

	[SerializeField] private float startingHealth;

	protected override void Awake()
	{
		base.Awake();

		CurrentHealth = startingHealth;
	}

	protected void OnEnable()
	{
		Entity.Events.AddListener<RefillHealthEvent>(OnRefillHealthEvent);

		GlobalEvents.AddListener<WeaponDamageEvent>(OnDamageEvent);
	}

	protected void OnDisable()
	{
		Entity.Events.RemoveListener<RefillHealthEvent>(OnRefillHealthEvent);

		GlobalEvents.RemoveListener<WeaponDamageEvent>(OnDamageEvent);
	}

	private void OnDamageEvent(WeaponDamageEvent evt)
	{
		if(evt.Damage.hit.target == Entity)
		{
			CurrentHealth -= evt.Damage.amount;
		}
	}

	private void OnRefillHealthEvent(RefillHealthEvent evt)
	{
		CurrentHealth = StartingHealth;
	}

#if UNITY_EDITOR
	[ContextMenu("Heal")]
	private void Heal()
	{
		CurrentHealth = StartingHealth;
	}

	[ContextMenu("Damage (1)")]
	private void Damage1()
	{
		Damage(1);
	}

	[ContextMenu("Damage (5)")]
	private void Damage5()
	{
		Damage(5);
	}

	private void Damage(float damage)
	{
		HitInfo hitInfo = new HitInfo(Entity, Entity, Vector3.zero, Vector3.zero, "Untagged");
		DamageInfo damageInfo = new DamageInfo(hitInfo, damage);

		GlobalEvents.Invoke(new WeaponDamageEvent(null, damageInfo));
	}
#endif
}