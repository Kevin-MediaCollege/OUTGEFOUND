using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Aim Down Sight")]
public class FirearmAimDownSight : MonoBehaviour
{
	[SerializeField] private Transform normal;
	[SerializeField] private Transform ads;

	private Entity entity;
	private Weapon weapon;

	private bool adsDuringReload = false;
	private bool reloading = false;

	protected void Awake()
	{
		entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
		adsDuringReload = false;
		reloading = false;
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<ToggleAimDownSightEvent>(OnToggleAimDownSightEvent);
		entity.Events.AddListener<ReloadEvent>(OnReloadEvent);
		entity.Events.AddListener<ReloadDoneEvent>(OnReloadDoneEvent);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<ToggleAimDownSightEvent>(OnToggleAimDownSightEvent);
		entity.Events.RemoveListener<ReloadEvent>(OnReloadEvent);
		entity.Events.RemoveListener<ReloadDoneEvent>(OnReloadDoneEvent);
	}

	private void OnToggleAimDownSightEvent(ToggleAimDownSightEvent evt)
	{
		if(AssignWeapon() && !reloading)
		{
			if(weapon.transform.parent == normal)
			{
				GlobalEvents.Invoke(new StartAimDownSightEvent());
				adsDuringReload = false;
				weapon.transform.SetParent(ads, false);
			}
			else
			{
				GlobalEvents.Invoke(new StopAimDownSightEvent());
				adsDuringReload = false;
				weapon.transform.SetParent(normal, false);
			}
		}
	}

	private void OnReloadEvent(ReloadEvent evt)
	{
		reloading = true;
		if(AssignWeapon())
		{
			if(weapon.transform.parent != normal)
			{
				GlobalEvents.Invoke(new StopAimDownSightEvent());
				adsDuringReload = true;
				weapon.transform.SetParent(normal, false);
			}
		}
	}

	private void OnReloadDoneEvent(ReloadDoneEvent evt)
	{
		reloading = false;
		if(AssignWeapon())
		{
			if(adsDuringReload)
			{
				GlobalEvents.Invoke(new StartAimDownSightEvent());

				weapon.transform.SetParent(ads, false);
			}
		}
	}

	private bool AssignWeapon()
	{
		if(weapon != null)
		{
			return true;
		}

		weapon = entity.GetComponentInChildren<Weapon>();
		return weapon != null;
	}
}