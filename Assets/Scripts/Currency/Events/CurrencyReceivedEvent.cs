public class CurrencyReceivedEvent : IEvent
{
	public int Amount { private set; get; }

	public CurrencyReceivedEvent(int amount)
	{
		Amount = amount;
	}
}