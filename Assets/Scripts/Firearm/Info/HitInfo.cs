using UnityEngine;

/// <summary>
/// Contains information about a gunshot
/// </summary>
public struct HitInfo
{
	public Entity Source { private set; get; }
	public Entity Target { set; get; }

	public Vector3 Direction { set; get; }
	public Vector3 Point { set; get; }
	public Vector3 Normal { set; get; }

	public string Tag { set; get; }

	public bool Hit
	{
		get
		{
			return Target != null && Target.GetComponent<Damagable>() != null;
		}
	}

	public HitInfo(Entity source) : this(source, null)
	{
	}

	public HitInfo(Entity source, Entity target)
	{
		Source = source;
		Target = target;

		Direction = Vector3.zero;
		Point = Vector3.zero;
		Normal = Vector3.zero;

		Tag = "";
	}
}