using UnityEngine;
using System.Collections;
using System;

public class Crosshair : MonoBehaviour
{
	[Serializable]
	private class CrosshairPart
	{
		[SerializeField] public RectTransform transform;
		[SerializeField] public Vector2 direction;
		[HideInInspector] public Vector2 original;
	}

	[SerializeField] private CrosshairPart[] parts;

	private CharacterController characterController;

	private Entity player;
	private Firearm firearm;

	protected void Awake()
	{
		foreach(CrosshairPart part in parts)
		{
			part.original = part.transform.anchoredPosition3D;
		}
	}

	protected void LateUpdate()
	{
		if(player == null || characterController == null)
		{
			player = EntityUtils.GetEntityWithTag("Player");
			characterController = player.GetComponent<CharacterController>();

			if(player == null || characterController == null)
			{
				return;
			}
		}

		if(firearm == null)
		{
			firearm = player.GetComponentInChildren<Firearm>();

			if(firearm == null)
			{
				return;
			}
		}

		foreach(CrosshairPart part in parts)
		{
			Vector2 target = part.original;

			float offset = firearm.RecoilOffset * 300;
			target += part.direction * offset;
			target += part.direction * characterController.velocity.magnitude * 3;

			part.transform.anchoredPosition3D = target;	
		}
	}
}