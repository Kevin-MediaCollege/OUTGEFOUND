using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Components/Firearm Audio")]
public class FirearmAudio : WeaponComponent
{
	private static float lastGunshotTime;

	[SerializeField] private AudioAsset gunshot;
	[SerializeField] private AudioAsset reload;
	[SerializeField] private AudioAsset emptyClip;

	[SerializeField] private Vector2 gunshotPitchRange;

	[SerializeField] private bool interval;
	[SerializeField] private float gunshotInterval;

	private AudioManager audioManager;
	private AudioChannel emptyClipAudioChannel;

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
			if(emptyClipAudioChannel != null && emptyClipAudioChannel.AudioAsset == emptyClip && emptyClipAudioChannel.IsPlaying)
			{
				return;
			}

			emptyClipAudioChannel = audioManager.PlayAt(emptyClip, ((Firearm)Weapon).Position);
		}
	}

	public override void OnFire(HitInfo hit)
	{
		bool canFire = true;

		if(interval)
		{
			if(Time.time - lastGunshotTime < gunshotInterval)
			{
				canFire = false;
			}
			else
			{
				lastGunshotTime = Time.time;
			}
		}

		if(canFire)
		{		
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
		emptyClipAudioChannel = audioManager.PlayAt(emptyClip, ((Firearm)Weapon).Position);
	}
}