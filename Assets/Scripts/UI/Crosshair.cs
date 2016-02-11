using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
	//private Firearm firearm;

	protected void Awake()
	{
	/*	Weapon weapon = EntityUtils.GetEntityWithTag("Player").Weapon;

		if(weapon.GetType() == typeof(Firearm))
		{
			firearm = weapon as Firearm;
		}*/
	}

	protected void FixedUpdate()
	{
		/*if(firearm == null)
		{
			return;
		}

		RaycastHit hit;
		if(Physics.Raycast(firearm.Barrel.position, firearm.Barrel.forward, out hit, 100))
		{
			Vector2 point = Camera.main.WorldToViewportPoint(hit.point);

			RectTransform rt = transform as RectTransform;
			rt.anchorMin = point;
			rt.anchorMax = point;
		}*/
	}
}