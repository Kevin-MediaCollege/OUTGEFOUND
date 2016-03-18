using UnityEngine;
using System.Collections;

public class FirearmSway : MonoBehaviour
{
	/*
	private Entity player;

	private Transform weapon;
	private Transform eyes;

	private Vector3 delay = new Vector3(0f, 0f, 0f);
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

			delay = new Vector3(0f, 0f, 0f);
			previous = new Vector2(eyes.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y);
			target = previous;
		}

		previous = target;
		float targetX = eyes.rotation.eulerAngles.x;
		float targetY = player.transform.rotation.eulerAngles.y;
		target = new Vector2(targetX, targetY);
		delay = Vector3.Slerp (delay, new Vector3 (previous.x - target.x, previous.y - target.y, 0f), 0.05f);
	}

	private IEnumerator	Lerp()
	{
		while(true)
		{
			if(weapon != null)
			{
				//Quaternion newRotation = Quaternion.Euler(delay);
				//weapon.localRotation.eulerAngles = delay;//Quaternion.Slerp(weapon.localRotation, newRotation, (Time.time * 0.1f));

				Quaternion rot = weapon.localRotation;
				Debug.Log (delay.x + "");
				rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z);
				weapon.localRotation = rot;
			}

			yield return null;
		}
	}
	*/
}