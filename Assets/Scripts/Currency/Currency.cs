using System;
using System.Collections.Generic;

public class Currency : IDependency
{
	public const int STARTING_AMOUNT = 0;

	public int Amount { private set; get; }

	public Currency()
	{
		Amount = STARTING_AMOUNT;
	}

	public void Add(int amount)
	{
		Amount += amount;
	}

	public void Remove(int amount)
	{
		Amount -= amount;
	}
}