using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Recoil")]
public class FirearmRecoil : WeaponComponent
{
	[SerializeField] private float recoil;

	private Animator animator;

	protected override void Awake()
	{
		base.Awake();

		animator = GetComponentInParent<Animator>();
	}

	public override void OnFire(HitInfo hit)
	{
		animator.SetTrigger("Shooting");
	}
}