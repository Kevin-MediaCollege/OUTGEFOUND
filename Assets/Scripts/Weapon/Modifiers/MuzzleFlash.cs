using UnityEngine;
using System.Collections;

public class MuzzleFlash : BaseWeaponModifier
{
	[SerializeField] private SpriteRenderer muzzleFlash;
	[SerializeField] private float showTime;

	public override void OnFire(ref DamageInfo info)
	{
		muzzleFlash.enabled = true;

		StartCoroutine("ShowMuzzleFlash");
	}

	private IEnumerator ShowMuzzleFlash()
	{
		yield return new WaitForSeconds(showTime);

		muzzleFlash.enabled = false;
	}
}