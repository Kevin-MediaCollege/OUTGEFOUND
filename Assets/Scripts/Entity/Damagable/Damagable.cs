using UnityEngine;
using System.Collections;
using System;

public class Damagable : MonoBehaviour, IEntityInjector
{
	public delegate void OnDamageReceived(ShotInfo shotInfo);
	public event OnDamageReceived onDamageReceivedEvent = delegate { };

	public Entity Entity { private set; get; }

	public void RegisterEntity(Entity entity)
	{
		Entity = entity;
	}

	public void Damage(ShotInfo shotInfo)
	{
		onDamageReceivedEvent(shotInfo);
	}
}