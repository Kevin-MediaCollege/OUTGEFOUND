using System.Collections;
using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm AI Delay")]
public class FirearmDelayer : WeaponComponent
{
	[SerializeField] private Vector2 delay;

	private bool canFire;

	protected override void Awake()
	{
		base.Awake();

		canFire = true;
	}

	public override void OnFire(HitInfo hit)
	{
		StartCoroutine("Delay");
	}

	public override bool CanFire()
	{
		return canFire;
	}

	private IEnumerator Delay()
	{
		canFire = false;

		yield return new WaitForSeconds(Random.Range(delay.x, delay.y));

		canFire = true;
	}
}