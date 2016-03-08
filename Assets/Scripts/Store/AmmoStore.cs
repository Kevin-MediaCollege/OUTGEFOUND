/// <summary>
/// The ammo store
/// </summary>
public class AmmoStore : Store
{
	protected override void Purchase()
	{
		Entity player = EntityUtils.GetEntityWithTag("Player");
		player.Events.Invoke(new RefillAmmoEvent());
	}
}
