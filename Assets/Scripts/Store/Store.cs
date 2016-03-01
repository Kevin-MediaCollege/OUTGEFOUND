using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour
{
	private bool inRange;

	protected void OnTriggerEnter(Collider collider)
	{
		Entity entity = collider.GetComponent<Entity>();

		if(entity != null && entity.HasTag("Player"))
		{
			GlobalEvents.Invoke(new StoreUIEvent(true));
			inRange = true;
		}
	}

	protected void OnTriggerExit(Collider collider)
	{
		Entity entity = collider.GetComponent<Entity>();

		if(entity != null && entity.HasTag("Player"))
		{
			GlobalEvents.Invoke(new StoreUIEvent(false));
			inRange = false;
		}
	}

	protected void Update()
	{
		if(inRange && Input.GetKeyDown(KeyCode.F))
		{
			TryPurchase();
		}
	}

	private void TryPurchase()
	{
		Debug.Log("TODO: Refill health and ammo if the player has enough points");
	}
}