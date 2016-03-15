using UnityEngine;

[AddComponentMenu("Weapon/Weapon Spawner")]
public class WeaponSpawner : MonoBehaviour
{
	[SerializeField] private Transform parent;
	[SerializeField] private Weapon weapon;

	protected void Awake()
	{
		GameObject weapon = Instantiate(this.weapon.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		weapon.transform.SetParent(parent, false);
		weapon.SetActive(true);

		Destroy(this);
	}
}