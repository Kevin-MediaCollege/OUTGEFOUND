using UnityEngine;
using System.Collections;

public interface IWeaponModifier
{
	void OnFire();

	bool CanFire();
}