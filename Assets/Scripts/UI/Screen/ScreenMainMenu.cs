using UnityEngine;
using System.Collections;

public class ScreenMainMenu : ScreenBase 
{
	public Touchable button_game;

	public override void OnScreenEnter()
	{
		
	}

	public override IEnumerator OnScreenFadein()
	{
		yield break;
	}

	public override IEnumerator OnScreenFadeout()
	{
		yield break;
	}

	public override void OnScreenExit()
	{
		
	}

	public override string getScreenName()
	{
		return "ScreenMainMenu";
	}
}
