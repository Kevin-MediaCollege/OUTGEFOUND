using UnityEngine;
using System.Collections;

public interface IHealthModifier
{
	HitInfo OnDamageReceived(HitInfo hitInfo);
}