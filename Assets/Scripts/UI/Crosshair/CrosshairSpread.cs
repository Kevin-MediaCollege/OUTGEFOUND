using UnityEngine;
using System;

public class CrosshairSpread : MonoBehaviour
{
	[Serializable]
	private class CrosshairPart
	{
		[SerializeField] public RectTransform transform;
		[SerializeField] public Vector2 direction;
		[NonSerialized] public Vector2 originalPosition;
	}

	[SerializeField] private CrosshairPart[] parts;

	private CrosshairSpreadComponent[] components;

	protected void Awake()
	{
		components = GetComponentsInChildren<CrosshairSpreadComponent>();

		foreach(CrosshairPart part in parts)
		{
			part.originalPosition = part.transform.anchoredPosition;
		}
	}

	protected void LateUpdate()
	{
		float spread = 0;

		foreach(CrosshairSpreadComponent component in components)
		{
			spread += component.Spread;
		}

		foreach(CrosshairPart part in parts)
		{
			part.transform.anchoredPosition = part.originalPosition + (part.direction * spread);
		}
	}
}