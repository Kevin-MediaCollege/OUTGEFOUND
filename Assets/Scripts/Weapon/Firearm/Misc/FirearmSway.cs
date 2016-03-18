using UnityEngine;
using System.Collections;

public class FirearmSway : MonoBehaviour
{
	private Entity player;

	private Transform weapon;
	private Transform eyes;

	private Vector2 start;
	private Vector2 target;
	private Vector2 previous;

	protected void OnEnable()
	{
		StartCoroutine("Lerp");
	}

	protected void OnDisable()
	{
		StopCoroutine("Lerp");
	}

	protected void Update()
	{
		if(player == null || weapon == null)
		{
			player = EntityUtils.GetEntityWithTag("Player");

			if(player == null)
			{
				return;
			}

			weapon = player.GetComponentInChildren<Weapon>().transform.parent;
			eyes = player.transform.Find("Eyes");

			start = new Vector2(eyes.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y);
		}

		float targetX = eyes.rotation.eulerAngles.x;
		float targetY = player.transform.rotation.eulerAngles.y;

		target = new Vector2(targetX - previous.x, targetY - previous.y);
		previous = new Vector2(targetX, targetY);
	}

	private IEnumerator	Lerp()
	{
		while(true)
		{
			if(weapon != null)
			{
				target = Vector3.Slerp(target, start, Time.time * 0.1f);

				Quaternion newRotation = Quaternion.Euler(target);
				weapon.localRotation = Quaternion.Slerp(weapon.localRotation, newRotation, (Time.time * 0.1f));
			}

			yield return null;
		}
	}
}