using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Serialization;

public class ScreenMainMenu : ScreenBase 
{
	public override string Name
	{
		get
		{
			return "ScreenMainMenu";
		}
	}

	[SerializeField] private Touchable gameButton;

	protected void Update()
	{
		if(Input.anyKeyDown)
		{
			//GameDependency game = Dependency.Get<GameDependency>();
			//game.Start();
		}
	}

	public override void OnScreenEnter()
	{
	}

	public override void OnScreenExit()
	{
	}

	public override IEnumerator OnScreenFadeIn()
	{
		yield break;
	}

	public override IEnumerator OnScreenFadeOut()
	{
		yield break;
	}
}
