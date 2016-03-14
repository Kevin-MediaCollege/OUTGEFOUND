/// <summary>
/// Manage the amount of currency (points) the player has
/// </summary>
public class Currency : IGameDependency
{
	public int Amount { set; get; }

	public Currency()
	{
		Amount = 0;
	}

	public void Start()
	{
		GlobalEvents.AddListener<EntityDiedEvent>(OnEntityDeathEvent);
	}

	public void Stop()
	{
		GlobalEvents.RemoveListener<EntityDiedEvent>(OnEntityDeathEvent);
	}

	private void OnEntityDeathEvent(EntityDiedEvent evt)
	{
		Entity target = evt.Entity;

		// Reset if the player dies
		if(target.HasTag("Player"))
		{
			Amount = 0;
		}
		// Add currency if the target is an enemy
		else if(target.HasTag("Enemy") && evt.DamageInfo.Hit.Source.HasTag("Player"))
		{
			string tag = evt.DamageInfo.Hit.Tag;

			switch(tag)
			{
			case "Head":
				AddCurrency(250);
				break;
			default:
				AddCurrency(100);
				break;
			}
		}
	}

	private void AddCurrency(int amount)
	{
		Amount += amount;
		GlobalEvents.Invoke(new CurrencyReceivedEvent(amount));
	}
}