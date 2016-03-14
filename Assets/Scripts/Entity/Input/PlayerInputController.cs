using UnityEngine;

/// <summary>
/// Input controller for the player
/// </summary>
public class PlayerInputController : InputController
{
	public float InputX
	{
		get
		{
			return Input.GetAxis("Horizontal");
		}
	}

	public float InputZ
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

	public override bool Crouch
	{
		get
		{
			return Input.GetKey(KeyCode.C);
		}
	}

	protected void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Entity.Events.Invoke(new StartFireEvent());
		}
		else if(Input.GetMouseButtonUp(0))
		{
			Entity.Events.Invoke(new StopFireEvent());
		}

		if(Input.GetKeyDown(KeyCode.V))
		{
			Entity.Events.Invoke(new SwitchFireModeEvent());
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			Entity.Events.Invoke(new ReloadEvent());
		}
	}
}