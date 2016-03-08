using UnityEngine;
using System.Collections;

public abstract class FirearmAimController : MonoBehaviour
{
	[SerializeField] protected LayerMask layerMask;

	public abstract Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit);
}
