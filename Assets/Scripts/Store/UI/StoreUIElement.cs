using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreUIElement : MonoBehaviour
{
	[SerializeField] private StoreItem item;

	[SerializeField] private Text title;
	[SerializeField] private Text description;

	[SerializeField] private Text cost;

	protected void Awake()
	{
		title.text = item.Name;
		description.text = item.Description;
		cost.text = item.Cost.ToString();
	}
}