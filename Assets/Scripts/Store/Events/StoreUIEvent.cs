using System.Collections;

public class StoreUIEvent : IEvent
{
	public bool Enabled { private set; get; }

	public StoreUIEvent(bool enabled)
	{
		Enabled = enabled;
	}
}