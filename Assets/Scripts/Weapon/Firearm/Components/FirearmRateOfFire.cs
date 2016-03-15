using UnityEngine;
using System.Collections;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Rate of Fire")]
public class FirearmRateOfFire : WeaponComponent
{
	[SerializeField] private int roundsPerMinute;

	private bool canFire;

	protected override void Awake()
	{
		base.Awake();

		canFire = true;
	}

	public override void OnFire(HitInfo hit)
	{
		StartCoroutine("Cooldown");
	}

	public override bool CanFire()
	{
		return canFire;
	}

	private IEnumerator Cooldown()
	{
		canFire = false;

		yield return new WaitForSeconds(60f / roundsPerMinute);

		canFire = true;
	}
}