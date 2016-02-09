using UnityEngine;
using System.Collections;

public interface IWeaponModifier
{
	void OnFire(ref ShotInfo shotInfo);

	bool CanFire();
}