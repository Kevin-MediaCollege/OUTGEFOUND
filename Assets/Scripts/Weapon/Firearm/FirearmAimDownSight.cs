using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Aim Down Sight")]
public class FirearmAimDownSight : MonoBehaviour
{
	[SerializeField] private Transform normal;
	[SerializeField] private Transform ads;

	private Entity entity;
	private Weapon weapon;

	protected void Awake()
	{
		entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<ToggleAimDownSightEvent>(OnToggleAimDownSightEvent);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<ToggleAimDownSightEvent>(OnToggleAimDownSightEvent);
	}

	private void OnToggleAimDownSightEvent(ToggleAimDownSightEvent evt)
	{
		if(AssignWeapon())
		{
			if(weapon.transform.parent == normal)
			{
				GlobalEvents.Invoke(new StartAimDownSightEvent());

				weapon.transform.SetParent(ads, false);
			}
			else
			{
				GlobalEvents.Invoke(new StopAimDownSightEvent());

				weapon.transform.SetParent(normal, false);
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