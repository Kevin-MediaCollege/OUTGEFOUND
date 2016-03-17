using UnityEngine;

/// <summary>
/// Base movement controller
/// </summary>
public abstract class EntityMovement : MonoBehaviour
{
	public Entity Entity { private set; get; }

	public bool Jumping
	{
		get
		{
			return verticalSpeed > 0;
		}
	}

	public bool Falling
	{
		get
		{
			return verticalSpeed < 0;
		}
	}

	public bool Crouching
	{
		get
		{
			return crouching;
		}
	}

	[SerializeField] protected float speed;
	[SerializeField] protected float jumpSpeed;

	[SerializeField] private AudioAsset footstep;
	[SerializeField] private float footstepInterval;

	private AudioManager audioManager;

	protected float verticalSpeed;

	protected bool crouching;

	private float lastFootstepTime;

	protected virtual void Awake()
	{
		Entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
		audioManager = Dependency.Get<AudioManager>();
	}

	protected void PlayFootstep()
	{
		if(Time.time - lastFootstepTime >= footstepInterval)
		{
			lastFootstepTime = Time.time;
			AudioChannel ac = audioManager.PlayAt(footstep, Entity.transform.position);
			ac.Pitch = Random.Range(0.7f, 1.3f);
		}
	}
}