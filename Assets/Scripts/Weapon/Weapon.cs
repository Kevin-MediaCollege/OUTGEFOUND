using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }

	public WeaponUpgrade Upgrade { protected set; get; }

	public GameObject Model { private set; get; }

	[SerializeField] protected WeaponUpgrade baseUpgrade;
	[SerializeField] protected WeaponUpgrade[] upgradeData;

	private List<WeaponModifier> modifiers = new List<WeaponModifier>();
	
	protected virtual void Awake()
	{
		SetUpgrade(baseUpgrade);
	}

	protected virtual void OnEnable()
	{
		GlobalEvents.AddListener<StartWeaponFireEvent>(OnStartFireEvent);
		GlobalEvents.AddListener<StopWeaponFireEvent>(OnStopFireEvent);
	}

	protected virtual void OnDisable()
	{
		GlobalEvents.RemoveListener<StartWeaponFireEvent>(OnStartFireEvent);
		GlobalEvents.RemoveListener<StopWeaponFireEvent>(OnStopFireEvent);
	}

	public virtual void StartFire()
	{
	}

	public virtual void StopFire()
	{
	}

	public bool CanFire()
	{
		foreach(WeaponModifier modifier in modifiers)
		{
			if(!modifier.CanFire())
			{
				return false;
			}
		}

		return true;
	}

	public void AddModifier(WeaponModifier modifier)
	{
		modifiers.Add(modifier);
	}

	public void RemoveModifier(WeaponModifier modifier)
	{
		modifiers.Remove(modifier);
	}

	protected abstract HitInfo ConstructHitInfo();

	protected bool Fire()
	{
		if(!CanFire())
		{
			return false;
		}

		HitInfo hitInfo = ConstructHitInfo();
		GlobalEvents.Invoke(new WeaponFireEvent(this, hitInfo));

		if(hitInfo.Hit)
		{
			// TODO: Apply damage modifiers
			DamageInfo damageInfo = new DamageInfo(hitInfo, Upgrade.BaseDamage);
			GlobalEvents.Invoke(new DamageEvent(damageInfo));
		}

		return true;
	}

	protected virtual void SetUpgrade(WeaponUpgrade upgrade)
	{
		Upgrade = upgrade;

		if(Model != null)
		{
			Destroy(Model);
		}

		Model = Instantiate(upgrade.Model);
		Model.transform.SetParent(transform, false);
	}

	private void OnStartFireEvent(StartWeaponFireEvent evt)
	{
		if(evt.Entity == Entity)
		{
			StartFire();
		};
	}

	private void OnStopFireEvent(StopWeaponFireEvent evt)
	{
		if(evt.Entity == Entity)
		{
			StopFire();
		}
	}
}