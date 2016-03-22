using UnityEngine;

/// <summary>
/// A door
/// </summary>
public class Door : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private float range;

	protected void FixedUpdate()
	{
		bool open = false;

		foreach(Entity entity in Entity.All)
		{
			if(!entity.HasTag("Player") && !entity.HasTag("Enemy"))
			{
				continue;
			}

			float distance2 = (transform.position - entity.transform.position).sqrMagnitude;

			if(distance2 < range * range)
			{
				open = true;
				break;
			}
		}

		animator.SetBool("Open", open);
	}

	protected void OnDrawGizmos()
	{
		if(enabled)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position, range);
		}
	}
}