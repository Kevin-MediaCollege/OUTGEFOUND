/// <summary>
/// The health store
/// </summary>
public class HealthStore : Store
{
	protected override void Purchase()
	{
		Entity player = EntityUtils.GetEntityWithTag("Player");
		player.Events.Invoke(new RefillHealthEvent());
	}
}