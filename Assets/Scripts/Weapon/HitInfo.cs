using UnityEngine;

public struct ShotInfo
{
	public Entity Source { private set; get; }
	public Damagable Target { set; get; }

	public Vector3 Direction { set; get; }
	public Vector3 Point { set; get; }
	public Vector3 Normal { set; get; }

	public int Damage { set; get; }

	public bool HitDamagable
	{
		get
		{
			return Target != null;
		}
	}

	public ShotInfo(Entity source, Damagable target, Vector3 direction, Vector3 point, Vector3 normal, int damage)
	{
		Source = source;
		Target = target;

		Direction = direction;
		Point = point;
		Normal = normal;

		Damage = damage;
	}
}