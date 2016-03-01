using UnityEngine;
using System.Collections;

public class StoreUI : MonoBehaviour
{
	[SerializeField] private GameObject storeUI;

	protected void OnEnable()
	{
		GlobalEvents.AddListener<StoreUIEvent>(OnStoreUIEvent);

		storeUI.SetActive(false);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<StoreUIEvent>(OnStoreUIEvent);
	}

	private void OnStoreUIEvent(StoreUIEvent evt)
	{
		storeUI.SetActive(evt.Enabled);
	}
}
