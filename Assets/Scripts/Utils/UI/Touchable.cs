using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public enum TouchableState
{
	Normal,
	Pressed,
	Disabled
}

public class Touchable : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler
{
	//--------------------------------------------------------------------------------//
	public Sprite PressedSprite = null;
	public Sprite DisabledSprite = null;
	public bool CancelPointerWhenItExits = true;
	//--------------------------------------------------------------------------------//

	public delegate void OnPointerDownHandler (Touchable _sender, PointerEventData _eventData);
	public event OnPointerDownHandler OnPointerDownEvent = delegate {};

	public delegate void OnPointerUpHandler (Touchable _sender, PointerEventData _eventData);
	public event OnPointerUpHandler OnPointerUpEvent = delegate {};

	public delegate void OnPointerExitHandler (Touchable _sender, PointerEventData _eventData);
	public event OnPointerExitHandler OnPointerExitEvent = delegate {};

	public delegate void OnPointerMoveHandler (Touchable _sender, PointerEventData _eventData);
	public event OnPointerMoveHandler OnPointerMoveEvent = delegate {};

	public delegate void OnPointerEnterHandler (Touchable _sender, PointerEventData _eventData);
	public event OnPointerEnterHandler OnPointerEnterEvent = delegate {};

	public Canvas Canvas { get; private set; }
	public Camera Camera { get; private set; }
	public Image Image { get; private set; }
	public TouchableState State { get; private set; }

	public bool Interactable 
	{
		get { return State != TouchableState.Disabled; }
		set 
		{
			if (!value)
				SetState(TouchableState.Disabled);
			else if (value && State == TouchableState.Disabled)
				SetState(TouchableState.Normal);
		}
	}

	private Sprite NormalSprite = null;

	protected List<int> pointers = new List<int>();

	protected virtual void Start ()
	{
		Canvas = GetComponent<Graphic>().canvas;
		if (Canvas.renderMode != RenderMode.ScreenSpaceOverlay)
			Camera = Canvas.worldCamera;
		else
			Camera = null;

		if (GetComponent<Image>() != null)
			Image = GetComponent<Image>();
		if (Image != null)
			NormalSprite = Image.sprite;
	}

	public virtual void OnPointerDown (PointerEventData eventData)
	{
		if (!Interactable)
			return;

		SetState(TouchableState.Pressed);
		pointers.Add(eventData.pointerId);

		OnPointerDownEvent(this, eventData);
	}

	public virtual void OnPointerUp (PointerEventData eventData)
	{
		if (!pointers.Contains(eventData.pointerId))
			return;

		pointers.Remove(eventData.pointerId);

		if (!Interactable)
			return;

		SetState(TouchableState.Normal);
		OnPointerUpEvent(this, eventData);
	}

	public virtual void OnPointerEnter (PointerEventData eventData)
	{
		if (Interactable)
			OnPointerEnterEvent(this, eventData);
	}

	public virtual void OnPointerExit (PointerEventData eventData)
	{
		if (Interactable)
		{
			SetState(TouchableState.Normal);
			OnPointerExitEvent(this, eventData);
		}
	}

	public virtual void OnDrag (PointerEventData eventData)
	{
		if (!pointers.Contains(eventData.pointerId))
			return;

		if (Interactable)
			OnPointerMoveEvent(this, eventData);
	}

	protected virtual void SetState(TouchableState _state)
	{
		State = _state;
		if (_state == TouchableState.Normal)
		{
			if (Image != null && NormalSprite != null)
				Image.sprite = NormalSprite;
		}
		else if (_state == TouchableState.Pressed)
		{
			if (Image != null && PressedSprite != null)
				Image.sprite = PressedSprite;
		}
		else if (_state == TouchableState.Disabled)
		{
			if (Image != null && DisabledSprite != null)
				Image.sprite = DisabledSprite;
		}

		if (Image != null && Image.type == Image.Type.Simple)
			Image.SetNativeSize();
	}

	public Vector3 GetCanvasPos(PointerEventData eventData)
	{
		return Canvas.transform.InverseTransformPoint(GetWorldPos(eventData));
	}

	public Vector3 GetWorldPos(PointerEventData eventData)
	{
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, Camera, out pos);

		return transform.TransformPoint(pos);
	}
}
