using UnityEngine;

[AddComponentMenu("Weapon/Components/Weapon Damage Modifier")]
public class WeaponDamageModifier : WeaponComponent
{
	[SerializeField] private float head;
	[SerializeField] private float body;
	[SerializeField] private float limbs;

	public override float GetDamage(HitInfo hit, float damage)
	{
		switch(hit.tag)
		{
		case "Head":
			return damage * head;
		case "Body":
			return damage * body;
		case "Limbs":
			return damage * limbs;
		}

		return damage;
	}
}