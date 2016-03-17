using UnityEngine;
using System.Collections;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Shell Audio")]
public class FirearmShellAudio : WeaponComponent
{
	[SerializeField] private AudioAssetGroup shells;
	[SerializeField] private float delay;

	private AudioManager audioManager;

	protected override void Awake()
	{
		base.Awake();

		audioManager = Dependency.Get<AudioManager>();
	}

	public override void OnFire(HitInfo hit)
	{
		StopCoroutine("PlayShell");
		StartCoroutine("PlayShell");
	}

	private IEnumerator PlayShell()
	{
		yield return new WaitForSeconds(delay);

		audioManager.PlayRandomAt(shells, ((Firearm)Weapon).Position);
	}
}