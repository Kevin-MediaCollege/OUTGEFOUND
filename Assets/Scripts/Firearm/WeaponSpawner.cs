using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour
{
	[SerializeField] private Firearm startingWeapon;
	[SerializeField] private Transform anchor;

	protected void Start()
	{
		GameObject weapon = Instantiate(startingWeapon.gameObject);
		weapon.transform.SetParent(anchor, false);
		weapon.SetActive(true);
	}
}