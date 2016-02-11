using UnityEngine;

public class EntityHealth : EntityAddon
{
	public int Starting
	{
		get
		{
			return startingHealth;
		}
	}

	public int Current { private set; get; }

	[SerializeField] private int startingHealth;

	protected void Awake()
	{
		Current = startingHealth;
	}

	protected void OnEnable()
	{
		Entity.Damagable.onDamageReceivedEvent += OnDamageReceived;
	}

	protected void OnDisable()
	{
		Entity.Damagable.onDamageReceivedEvent -= OnDamageReceived;
	}

	public void Damage(int damage)
	{
		if(Current > 0)
		{
			// Make sure health doesn't go below zero
			damage = Mathf.Min(Current, damage);
			Current -= damage;
		}
	}

	private void OnDamageReceived(DamageInfo info)
	{
		Damage(info.Amount);
	}
}