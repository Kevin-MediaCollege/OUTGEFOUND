using UnityEngine;

[AddComponentMenu("Weapon/Firearm/Firearm")]
public class Firearm : Weapon
{
	public Vector3 Position
	{
		get
		{
			return barrel.position;
		}
	}

	public Quaternion Rotation
	{
		get
		{
			return barrel.rotation;
		}
	}

	[SerializeField] protected Transform barrel;

	[SerializeField] protected float range;

	protected override HitInfo GetHitInfo(Vector3 direction)
	{
		Ray ray = new Ray(Position, direction);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, range, layers))
		{
			Entity target = null;
			string tag = "Untagged";
			
			if(hit.collider != null)
			{
				target = hit.collider.GetComponentInParent<Entity>();
				tag = hit.collider.tag;
			}

			Debug.DrawRay(Position, direction * hit.distance, target != null ? Color.green : Color.red, 3);
			return new HitInfo(Wielder, target, hit.point, hit.normal, tag);
		}

		Debug.DrawRay(Position, direction * range, Color.red, 3);
		return new HitInfo(Wielder, null, Vector3.zero, Vector3.zero, "Untagged");
	}

	protected override Vector3 GetBaseAimDirection()
	{
		return aim.GetAimDirection(Position);
	}
}