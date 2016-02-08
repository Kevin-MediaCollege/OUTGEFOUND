using System.Collections;
using UnityEngine;

public struct HitInfo
{
	public Entity source;
	public Entity target;

	public Vector3 point;
	public Vector3 normal;

	public int damage;

	public HitInfo(Entity source, Entity target, Vector3 point, Vector3 normal, int damage)
	{
		this.source = source;
		this.target = target;

		this.point = point;
		this.normal = normal;

		this.damage = damage;
	}
}