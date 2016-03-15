using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Bullet Spread")]
public class FirearmBulletSpread : WeaponComponent
{
	[SerializeField] private float normalSpread;
	[SerializeField] private float adsSpread;

	private bool ads;

	protected override void OnEnable()
	{
		base.OnEnable();

		GlobalEvents.AddListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		GlobalEvents.AddListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		GlobalEvents.RemoveListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		GlobalEvents.RemoveListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	public override Vector3 GetAimDirection(Vector3 direction)
	{
		float spread = ads ? adsSpread : normalSpread;

		float x = Random.Range(-spread, spread);
		float y = Random.Range(-spread, spread);
		float z = Random.Range(-spread, spread);
		
		return direction + new Vector3(x, y, z);
	}

	private void OnStartAimDownSightEvent(StartAimDownSightEvent evt)
	{
		ads = true;
	}

	private void OnStopAimDownSightEvent(StopAimDownSightEvent evt)
	{
		ads = false;
	}
}