using UnityEngine;

public struct HitInfo
{
	public Entity Source { private set; get; }
	public Entity Target { set; get; }

	public Vector3 Direction { set; get; }
	public Vector3 Point { set; get; }
	public Vector3 Normal { set; get; }

	public bool Hit
	{
		get
		{
			EntityHealth health = Dependency.Get<EntityHealth>();
			return Target != null && health.CanDamage(Target);
		}
	}

	public HitInfo(Entity source, Entity target, Vector3 direction, Vector3 point, Vector3 normal)
	{
		Source = source;
		Target = target;

		Direction = direction;
		Point = point;
		Normal = normal;
	}
}