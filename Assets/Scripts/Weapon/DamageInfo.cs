using UnityEngine;

public struct DamageInfo
{
	public Entity Source { private set; get; }
	public Entity Target { set; get; }

	public Vector3 Direction { set; get; }
	public Vector3 Point { set; get; }
	public Vector3 Normal { set; get; }

	public int Amount { set; get; }

	public bool Hit
	{
		get
		{
			return Target != null && Target.Damagable != null;
		}
	}

	public DamageInfo(Entity source, Entity target, Vector3 direction, Vector3 point, Vector3 normal, int damage)
	{
		Source = source;
		Target = target;

		Direction = direction;
		Point = point;
		Normal = normal;

		Amount = damage;
	}
}