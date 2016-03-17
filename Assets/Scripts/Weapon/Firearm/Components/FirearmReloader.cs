using UnityEngine;
using System.Collections;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Reloader")]
public class FirearmReloader : WeaponComponent
{
	[SerializeField] private float reloadSpeed;

	private Stockpile stockpile;

	private bool reloading;
	private bool magazineEmpty;

	protected override void Awake()
	{
		base.Awake();

		stockpile = Weapon.GetComponentInChildren<Stockpile>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		Weapon.Wielder.Events.AddListener<AttemptReloadEvent>(OnAttemptReloadEvent);
		Weapon.Wielder.Events.AddListener<MagazineEmptyEvent>(OnMagazineEmptyEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		Weapon.Wielder.Events.RemoveListener<AttemptReloadEvent>(OnAttemptReloadEvent);
		Weapon.Wielder.Events.RemoveListener<MagazineEmptyEvent>(OnMagazineEmptyEvent);
	}

	public override void TryFire()
	{
		if(magazineEmpty && !reloading)
		{
			StartCoroutine("Reload");
		}
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

	private void OnMagazineEmptyEvent(MagazineEmptyEvent evt)
	{
		magazineEmpty = true;
	}

	private IEnumerator Reload()
	{
		if(stockpile.Empty)
		{
			yield break;
		}

		reloading = true;

		stockpile.Add(stockpile.Magazine.Remaining);
		stockpile.Magazine.Remove(stockpile.Magazine.Remaining);

		yield return new WaitForSeconds(reloadSpeed);

		reloading = false;
		magazineEmpty = false;

		stockpile.FillMagazine();

		Weapon.Wielder.Events.Invoke(new ReloadEvent());
	}
}