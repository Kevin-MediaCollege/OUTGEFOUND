using UnityEngine;

[AddComponentMenu("Weapon/Weapon Spawner")]
public class WeaponSpawner : MonoBehaviour
{
	[SerializeField] private Transform parent;
	[SerializeField] private string weaponName;

	protected void Start()
	{
		GameObject prefab = Resources.Load<GameObject>(weaponName);
		GameObject weapon = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

		weapon.transform.SetParent(parent, false);
		weapon.SetActive(true);
	}
}