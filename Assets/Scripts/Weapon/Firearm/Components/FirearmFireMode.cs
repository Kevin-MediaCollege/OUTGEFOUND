using UnityEngine;
using System;

[Flags]
public enum FireMode
{
	Automatic = 1,
	SemiAutomatic = 2,
	Burst3 = 4,
}

[AddComponentMenu("Weapon/Firearm/Components/Firearm Fire Mode")]
public class FirearmFireMode : WeaponComponent
{
	[SerializeField, EnumFlags] private FireMode fireModes;
	[SerializeField] private FireMode current;

	private int burstId;

	protected override void Awake()
	{
		base.Awake();

		if(!HasFireMode(current))
		{
			current = GetAvailableFireMode();
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		Weapon.Wielder.Events.AddListener<SwitchFireModeEvent>(OnSwitchFireModeEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		Weapon.Wielder.Events.RemoveListener<SwitchFireModeEvent>(OnSwitchFireModeEvent);
	}

	public override bool CanStopFire()
	{
		if(current == FireMode.Burst3)
		{
			return false;
		}

		return base.CanStopFire();
	}

	public override void OnStartFire()
	{
		burstId = 0;
	}

	public override void OnFire(HitInfo hit)
	{
		burstId++;

		switch(current)
		{
		case FireMode.SemiAutomatic:
			Weapon.StopFire(true);
			break;
		case FireMode.Burst3:
			UpdateBurst();
			break;
		}
	}

	private void OnSwitchFireModeEvent(SwitchFireModeEvent evt)
	{
		current = GetNextFireMode();
	}

	private void UpdateBurst()
	{
		if(current == FireMode.Burst3 && burstId == 3)
		{
			Weapon.StopFire(true);
		}
	}

	private FireMode GetAvailableFireMode()
	{
		if(HasFireMode(FireMode.Automatic))
		{
			return FireMode.Automatic;
		}

		if(HasFireMode(FireMode.Burst3))
		{
			return FireMode.Burst3;
		}

		if(HasFireMode(FireMode.SemiAutomatic))
		{
			return FireMode.SemiAutomatic;
		}

		throw new ArgumentException("No available fire modes");
	}

	private FireMode GetNextFireMode()
	{
		if(current == FireMode.Automatic)
		{
			return
				HasFireMode(FireMode.Burst3) ? FireMode.Burst3 :
				HasFireMode(FireMode.SemiAutomatic) ? FireMode.SemiAutomatic :
				FireMode.Automatic;
		}

		if(current == FireMode.Burst3)
		{
			return
				HasFireMode(FireMode.SemiAutomatic) ? FireMode.SemiAutomatic :
				HasFireMode(FireMode.Automatic) ? FireMode.Automatic :
				FireMode.Burst3;
		}

		if(current == FireMode.SemiAutomatic)
		{
			return
				HasFireMode(FireMode.Automatic) ? FireMode.Automatic :
				HasFireMode(FireMode.Burst3) ? FireMode.Burst3 :
				FireMode.SemiAutomatic;
		}

		throw new ArgumentException("No available fire modes");
	}

	private bool HasFireMode(FireMode target)
	{
		return (fireModes & target) == target;
	}
}