using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Audio")]
public class FirearmAudio : WeaponComponent
{
	private static float lastGunshotTime;

	[SerializeField] private AudioAsset gunshot;
	[SerializeField] private AudioAsset reload;
	[SerializeField] private AudioAsset emptyClip;

	[SerializeField] private Vector2 gunshotPitchRange;

	[SerializeField] private float gunshotInterval;

	private AudioManager audioManager;

	private bool magazineEmpty;

	protected override void Awake()
	{
		base.Awake();

		audioManager = Dependency.Get<AudioManager>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		Weapon.Wielder.Events.AddListener<ReloadEvent>(OnReloadEvent);
		Weapon.Wielder.Events.AddListener<MagazineEmptyEvent>(OnMagazineEmptyEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		Weapon.Wielder.Events.RemoveListener<ReloadEvent>(OnReloadEvent);
		Weapon.Wielder.Events.RemoveListener<MagazineEmptyEvent>(OnMagazineEmptyEvent);
	}

	public override void TryFire()
	{
		if(magazineEmpty)
		{
			audioManager.PlayAt(emptyClip, ((Firearm)Weapon).Position);
		}
	}

	public override void OnFire(HitInfo hit)
	{
		if(Time.time - lastGunshotTime > gunshotInterval)
		{
			lastGunshotTime = Time.time;

			AudioChannel audioChannel = audioManager.PlayAt(gunshot, ((Firearm)Weapon).Position);
			if(audioChannel != null)
			{
				audioChannel.Pitch = Random.Range(gunshotPitchRange.x, gunshotPitchRange.y);
			}
		}
	}

	private void OnReloadEvent(ReloadEvent evt)
	{
		audioManager.PlayAt(reload, ((Firearm)Weapon).Position);
		magazineEmpty = false;
	}

	private void OnMagazineEmptyEvent(MagazineEmptyEvent evt)
	{
		magazineEmpty = true;
	}
}