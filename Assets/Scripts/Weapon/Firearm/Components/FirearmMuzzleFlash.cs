using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Muzzle Flash")]
public class FirearmMuzzleFlash : WeaponComponent
{
	[Serializable]
	private struct MuzzleFlash
	{
		public Sprite flash;
		public Sprite smoke;
	}

	[SerializeField] private MuzzleFlash[] flashes;

	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private float duration;

	public override void OnFire(HitInfo hit)
	{
		StopCoroutine("DisplayMuzzleFlash");
		StartCoroutine("DisplayMuzzleFlash");
	}

	private IEnumerator DisplayMuzzleFlash()
	{
		MuzzleFlash target = flashes[UnityEngine.Random.Range(0, flashes.Length)];

		spriteRenderer.enabled = true;
		spriteRenderer.sprite = target.flash;

		yield return new WaitForSeconds(duration / 2);

		spriteRenderer.sprite = target.smoke;

		yield return new WaitForSeconds(duration / 2);

		spriteRenderer.enabled = false;
	}
}