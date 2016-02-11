using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{
	public override float InputX
	{
		get
		{
			return Input.GetAxis("Horizontal");
		}
	}

	public override float InputZ
	{
		get
		{
			return Input.GetAxis("Vertical");
		}
	}

	public override bool Jump
	{
		get
		{
			return Input.GetKey(KeyCode.Space);
		}
	}
}