/// <summary>
/// Contains information about the damage an entity received
/// </summary>
public struct DamageInfo
{
	public HitInfo Hit { private set; get; }

	public float Damage { set; get; }

	public DamageInfo(HitInfo hit, float damage)
	{
		Hit = hit;
		Damage = damage;
	}
}