using UnityEngine;

public abstract class Weapon : MonoBehaviour, IEntityInjector
{
	public delegate void OnFire(HitInfo hitInfo);
	public event OnFire onFireEvent = delegate { };

	public Entity Entity { set; get; }

	[SerializeField] private int damage;

	private EntityHealth health;
	private bool firing;

	protected void Awake()
	{
		health = Dependency.Get<EntityHealth>();
	}

	protected void FixedUpdate()
	{
		if(firing)
		{
			if(!CanFire())
			{
				return;
			}

			HitInfo hitInfo = GetHitInfo();
			onFireEvent(hitInfo);

			if(hitInfo.Hit)
			{
				DamageInfo damageInfo = new DamageInfo(hitInfo.Source, hitInfo.Target, damage);
				health.Damage(damageInfo);
			}
		}
	}

	public virtual void StartFire()
	{
		if(CanFire())
		{
			firing = true;
		}
	}

	public virtual void StopFire()
	{
		firing = false;
	}

	public bool CanFire()
	{
		return true;
	}

	protected abstract HitInfo GetHitInfo();	
}