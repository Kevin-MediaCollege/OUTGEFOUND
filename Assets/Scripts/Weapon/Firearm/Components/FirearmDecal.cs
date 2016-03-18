using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Decals")]
public class FirearmDecal : WeaponComponent
{
	private DecalManager decalManager;

	protected override void Awake()
	{
		base.Awake();

		decalManager = Dependency.Get<DecalManager>();
	}

	public override void OnFire(HitInfo hit)
	{
		decalManager.AddDecal(hit.point, hit.normal, hit.tag, hit.target);
	}
}