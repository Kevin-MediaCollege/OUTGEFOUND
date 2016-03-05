using UnityEngine;
using System.Collections;

public abstract class FirearmAimController : MonoBehaviour
{
	public abstract Vector3 GetAimDirection(Firearm firearm, out RaycastHit hit);
}
