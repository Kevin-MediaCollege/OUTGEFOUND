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
			return Input.GetAxisRaw("Jump") != 0;
		}
	}

	public override bool Crouch
	{
		get
		{
			if(!ToggleCrouch)
			{
				return Input.GetAxisRaw("Crouch") != 0;
			}
			else
			{
				if(Input.GetAxisRaw("Crouch") != 0)
				{
					if(!crouch)
					{
						crouch = true;
						return true;
					}
				}
				else if(Input.GetAxisRaw("Crouch") == 0)
				{
					crouch = false;
				}
			}

			return false;
		}
	}

	public bool ToggleADS { set; get; }

	public bool ToggleCrouch { set; get; }

	private bool switchFireMode;
	private bool firing;
	private bool ads;
	private bool reload;
	private bool crouch;

	protected void Update()
	{
		if(Input.GetAxisRaw("Fire") != 0)
		{
			if(!firing)
			{
				Entity.Events.Invoke(new StartFireEvent());
				firing = true;
			}
		}
		else if(Input.GetAxisRaw("Fire") == 0)
		{
			if(firing)
			{
				Entity.Events.Invoke(new StopFireEvent());
				firing = false;
			}
		}

		if(!ToggleADS)
		{
			if(Input.GetAxisRaw("ADS") != 0)
			{
				if(!ads)
				{
					Entity.Events.Invoke(new ToggleAimDownSightEvent());
					ads = true;
				}
			}
			else if(Input.GetAxisRaw("ADS") == 0)
			{
				if(ads)
				{
					Entity.Events.Invoke(new ToggleAimDownSightEvent());
					ads = false;
				}
			}
		}
		else
		{
			if(Input.GetAxisRaw("ADS") != 0)
			{
				if(!ads)
				{
					Entity.Events.Invoke(new ToggleAimDownSightEvent());
					ads = true;
				}
			}
			else if(Input.GetAxisRaw("ADS") == 0)
			{
				ads = false;
			}
		}

		if(Input.GetAxisRaw("SwitchFireMode") != 0)
		{
			if(!switchFireMode)
			{
				Entity.Events.Invoke(new SwitchFireModeEvent());
				switchFireMode = true;
			}
		}
		else if(Input.GetAxisRaw("SwitchFireMode") == 0)
		{
			switchFireMode = false;
		}

		if(Input.GetAxisRaw("Reload") != 0)
		{
			if(!reload)
			{
				Entity.Events.Invoke(new AttemptReloadEvent());
				reload = true;
			}
		}
		else if(Input.GetAxisRaw("Reload") == 0)
		{
			reload = false;
		}
	}
}