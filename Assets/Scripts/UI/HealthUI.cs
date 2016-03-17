using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private Image image;

	private Entity player;
	private EntityHealth health;

	protected void Start()
	{
		image.material = new Material(image.material);
	}

	protected void LateUpdate()
	{
		if(player == null || health == null)
		{
			player = EntityUtils.GetEntityWithTag("Player");

			if(player == null)
			{
				return;
			}

			health = player.GetHealth();

			if(health == null)
			{
				return;
			}
		}

		float current = health.CurrentHealth;
		float max = health.StartingHealth;

		float alpha = 1 - current / max;

		Color color = image.material.color;
		color.a = alpha * 0.65f;
		image.material.color = color;
	}
}
