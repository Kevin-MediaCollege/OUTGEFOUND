using System;
using UnityEngine;

public class CrosshairSpreadVelocity : CrosshairSpreadComponent
{
	public override float Spread
	{
		get
		{
			if(AssignCharacterController())
			{
				return characterController.velocity.magnitude;
			}

			return 0;
		}
	}

	private Entity entity;
	private CharacterController characterController;

	private bool AssignCharacterController()
	{
		if(characterController != null)
		{
			return true;
		}

		if(entity == null)
		{
			entity = EntityUtils.GetEntityWithTag("Player");

			if(entity == null)
			{
				return false;
			}
		}

		characterController = entity.GetComponentInParent<CharacterController>();
		return characterController != null;
	}
}