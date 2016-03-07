using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour
{
	[SerializeField] private Firearm startingWeapon;
	[SerializeField] private Transform anchor;

	protected void Start()
	{
		Entity entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
		bool isPlayer = entity.HasTag("Player");

		GameObject weapon = Instantiate(startingWeapon.gameObject);
		weapon.transform.SetParent(anchor, false);
		weapon.SetActive(true);

		if(isPlayer)
		{
			weapon.transform.localScale *= 2;

			int layer = LayerMask.NameToLayer("Player Weapon");

			foreach(Transform t in weapon.GetComponentsInChildren<Transform>(true))
			{
				t.gameObject.layer = layer;
			}
		}
	}
}