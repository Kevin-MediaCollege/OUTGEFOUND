using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonDefault : MonoBehaviour 
{
	public Image top;
	public Image bottom;
	public Touchable touchable;

	void Start () 
	{
		Debug.Log("init");
		onExit(null, null);
		touchable.OnPointerEnterEvent += onEnter;
		touchable.OnPointerExitEvent += onExit;
	}

	void onEnter (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		top.color = new Color32(255, 255, 255, 255);
		bottom.color = new Color32(255, 255, 255, 255);
	}

	void onExit (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		top.color = new Color32(0, 0, 0, 0);
		bottom.color = new Color32(0, 0, 0, 0);
	}
}
