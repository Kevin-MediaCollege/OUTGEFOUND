using UnityEngine;
using System.Collections;

public abstract class Store : MonoBehaviour
{
	[SerializeField] private Canvas storeUI;
	[SerializeField] private int price;

	private Currency currency;

	private bool inRange;

	protected void Awake()
	{
		currency = Dependency.Get<Currency>();
		storeUI.enabled = false;
	}

	protected void Update()
	{
		if(inRange && Input.GetKeyDown(KeyCode.F))
		{
			TryPurchase();
		}
	}

	protected void OnTriggerEnter(Collider collider)
	{
		OnTriggerUpdate(collider, true);
	}

	protected void OnTriggerExit(Collider collider)
	{
		OnTriggerUpdate(collider, false);
	}

	protected virtual bool CanPurchase()
	{
		return true;
	}

	protected virtual void Purchase()
	{
	}

	private void OnTriggerUpdate(Collider collider, bool entered)
	{
		Entity entity = collider.GetComponent<Entity>();

		if(entity != null && entity.HasTag("Player"))
		{
			storeUI.enabled = entered;
			inRange = entered;
		}
	}

	private void TryPurchase()
	{
		if(currency.Amount >= price && CanPurchase())
		{
			Purchase();

			currency.Amount -= price;
		}
	}
}
