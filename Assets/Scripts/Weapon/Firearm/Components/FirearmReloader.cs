using UnityEngine;
using System.Collections;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Reloader")]
public class FirearmReloader : WeaponComponent
{
	[SerializeField] private float reloadSpeed;

	private Stockpile stockpile;

	private bool reloading;

	protected override void Awake()
	{
		base.Awake();

		stockpile = Weapon.GetComponentInChildren<Stockpile>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		Weapon.Wielder.Events.AddListener<AttemptReloadEvent>(OnAttemptReloadEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		Weapon.Wielder.Events.RemoveListener<AttemptReloadEvent>(OnAttemptReloadEvent);
	}

	public override bool CanFire()
	{
		return !reloading;
	}

	private void OnAttemptReloadEvent(AttemptReloadEvent evt)
	{
		if(stockpile.Magazine.Full || reloading)
		{
			return;
		}

		StartCoroutine("Reload");
	}

	private IEnumerator Reload()
	{
		Weapon.Wielder.Events.Invoke(new ReloadEvent());

		reloading = true;

		stockpile.Add(stockpile.Magazine.Remaining);
		stockpile.Magazine.Remove(stockpile.Magazine.Remaining);

		yield return new WaitForSeconds(reloadSpeed);

		reloading = false;

		stockpile.FillMagazine();
	}
}