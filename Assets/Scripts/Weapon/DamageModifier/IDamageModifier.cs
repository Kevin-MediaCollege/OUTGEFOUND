using UnityEngine;
using System.Collections;

public interface IDamageModifier
{
	HitInfo Modify(HitInfo hitInfo);
}