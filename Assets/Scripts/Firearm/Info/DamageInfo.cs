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