using UnityEngine;
using System.Collections;

public class HearthbeatAudio : MonoBehaviour
{
	[SerializeField] private new AudioAsset audio;

	private AudioManager audioManager;
	private AudioChannel audioChannel;

	private Entity player;
	private EntityHealth health;

	protected void Awake()
	{
		audioManager = Dependency.Get<AudioManager>();
	}

	protected void Update()
	{
		if(player == null || health == null)
		{
			player = EntityUtils.GetEntityWithTag("Player");

			if(player == null)
			{
				return;
			}

			health = player.GetHealth();
		}

		if(health.CurrentHealth <= (health.StartingHealth / 4))
		{
			if(audioChannel == null || !audioChannel.IsPlaying)
			{
				audioChannel = audioManager.Play(audio);
			}
		}
	}
}