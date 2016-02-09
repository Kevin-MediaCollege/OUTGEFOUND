using UnityEngine;
using System.Collections;

public interface IHealthModifier
{
	void OnDamageReceived(ref ShotInfo shotInfo);
}