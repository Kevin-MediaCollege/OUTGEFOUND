using UnityEngine;
using System.Collections;

public interface IWeaponModifier
{
	void OnFire(ref DamageInfo info);

	bool CanFire();
}