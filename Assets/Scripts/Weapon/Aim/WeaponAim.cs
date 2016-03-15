using UnityEngine;

public abstract class WeaponAim : MonoBehaviour
{
	public abstract Vector3 GetAimDirection(Vector3 position);
}