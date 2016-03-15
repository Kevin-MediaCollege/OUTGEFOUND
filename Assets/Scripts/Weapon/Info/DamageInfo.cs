public struct DamageInfo
{
	public HitInfo hit;

	public float amount;

	public DamageInfo(HitInfo hit, float amount)
	{
		this.hit = hit;
		this.amount = amount;
	}
}