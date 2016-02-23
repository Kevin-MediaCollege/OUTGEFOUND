using UnityEngine;
using System.Collections;

public class FirearmMuzzleFlash : WeaponComponent
{
	[SerializeField] private SpriteRenderer muzzleFlash;
	[SerializeField] private float duration;

	protected override void OnFire(HitInfo hitInfo)
	{
		StopCoroutine("DisableMuzzleFlash");
		StartCoroutine("DisableMuzzleFlash");
	}

	private IEnumerator DisableMuzzleFlash()
	{
		Vector3 euler = muzzleFlash.transform.eulerAngles;
		euler.z = Random.Range(0f, 360f);
		muzzleFlash.transform.eulerAngles = euler;

		muzzleFlash.enabled = true;

		yield return new WaitForSeconds(duration);

		muzzleFlash.enabled = false;
	}
}