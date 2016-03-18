using System.Collections;
using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Recoil")]
public class FirearmRecoil : WeaponComponent
{
	[SerializeField] private float recoil;
	[SerializeField] private float decreaseAmount;
	[SerializeField] private float stopFireDecreaseAmount;

	[SerializeField] private float decreaseSpeed;

	private Animator animator;

	private float y;

	protected override void Awake()
	{
		base.Awake();

		animator = GetComponentInParent<Animator>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		StartCoroutine("Decrease");
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		StopCoroutine("Decrease");
	}

	protected void Update()
	{
		GameCamera.offset.y += y;
	}

	public override void OnFire(HitInfo hit)
	{
		animator.SetTrigger("Shooting");
		y += recoil;
	}

	public override void OnStopFire()
	{
		y -= stopFireDecreaseAmount;
		y = Mathf.Max(y, 0);
	}

	public override Vector3 GetAimDirection(Vector3 direction)
	{
		direction.y += y;
		return direction;
	}

	private IEnumerator Decrease()
	{
		while(true)
		{
			y -= decreaseAmount;
			y = Mathf.Max(y, 0);

			yield return new WaitForSeconds(decreaseSpeed);
		}
	}
}