using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComponentSlider : MonoBehaviour
{
	public RectTransform component;
	public RectTransform filler;
	public Touchable touchable;
	public Text valueText;

	public float valueStart = 60f;
	public float valueEnd = 90f;
	public bool roundValue;

	private float currentValue;

	void Awake()
	{
		touchable.OnPointerDownEvent += onButton;
		touchable.OnPointerUpEvent += onButton;
		touchable.OnPointerMoveEvent += onButton;

		if(valueStart > valueEnd)
		{
			throw new UnityException ("Error: start value is higher than end value");
		}
	}

	public void init(float _value)
	{
		_value = Mathf.Clamp01 (_value);
		filler.sizeDelta = new Vector2(400f * _value, 15f);
		currentValue = Mathf.Clamp01((_value - valueStart) / valueEnd);
		updateText ();
	}

	void onButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
	{
		float rawPos = Mathf.Clamp ((_eventData.position.x - (Screen.width / 2f)) - (filler.anchoredPosition.x + component.anchoredPosition.x), 0f, 400f);
		filler.sizeDelta = new Vector2(rawPos, 15f);
		currentValue = Mathf.Clamp01(rawPos / 400f);
		updateText ();
	}

	public float getFloat()
	{
		return valueStart + convertValue(currentValue);
	}

	private float convertValue(float _value)
	{
		return (valueEnd - valueStart) * _value;
	}

	private void updateText()
	{
		if (roundValue) 
		{
			valueText.text = "" + Mathf.RoundToInt(valueStart + convertValue (currentValue));
		} 
		else 
		{
			valueText.text = "" + (valueStart + convertValue (currentValue));
		}
	}
}
