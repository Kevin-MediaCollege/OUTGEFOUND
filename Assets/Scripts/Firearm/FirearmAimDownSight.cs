using UnityEngine;

public class FirearmAimDownSight : MonoBehaviour
{
	[SerializeField] private Transform normal;
	[SerializeField] private Transform ads;

	private Firearm firearm;
	private Entity entity;

	protected void Awake()
	{
		entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		entity.Events.AddListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		entity.Events.RemoveListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	private void OnStartAimDownSightEvent(StartAimDownSightEvent evt)
	{
		if(AssignFirearm())
		{
			firearm.transform.SetParent(ads, false);
			Dependency.Get<AimDownSightController>().IsAimingDownSight = true;
		}	
	}

	private void OnStopAimDownSightEvent(StopAimDownSightEvent evt)
	{
		if(AssignFirearm())
		{
			firearm.transform.SetParent(normal, false);
			Dependency.Get<AimDownSightController>().IsAimingDownSight = false;
		}
	}

	private bool AssignFirearm()
	{
		if(firearm != null)
		{
			return true;
		}

		firearm = GetComponentInChildren<Firearm>();
		return firearm != null;
	}
}