using UnityEngine;
using System.Collections;

public class Waterbarrel : MonoBehaviour
{
	[SerializeField] private Entity entity;
	[SerializeField] private new Rigidbody rigidbody;

	protected void OnEnable()
	{
		entity.Events.AddListener<WeaponDamageEvent>(OnWeaponDamageEvent);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<WeaponDamageEvent>(OnWeaponDamageEvent);
	}

	private void OnWeaponDamageEvent(WeaponDamageEvent evt)
	{
		rigidbody.AddForce(-evt.Hit.normal * 7, ForceMode.Impulse);
	}
}