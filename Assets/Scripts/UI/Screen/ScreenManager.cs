using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour 
{
	private static ScreenManager instance;
	public static ScreenManager Instance
	{
		get
		{
			return instance;
		}
	}

	public ScreenBase CurrentScreen { private set; get; }

	[SerializeField] private ScreenBase[] screenList;	
	[SerializeField] private string startScreen;

	private int screenListLenght;

	private bool isSwitching;

	protected void Awake()
	{
		instance = this;
		isSwitching = false;

		screenListLenght = screenList.Length;
		for(int i = 0; i < screenListLenght; i++)
		{
			screenList [i].gameObject.SetActive (false);
		}

		SetScreen(startScreen);
	}

	public void SetScreen(string name, bool skipFadeout = false)
	{
		if(!isSwitching)
		{
			ScreenBase screen = GetScreenByName (name);
			if(screen != null)
			{
				isSwitching = true;
				StartCoroutine (SwitchScreen (screen, skipFadeout));
			}
		}
	}

	private IEnumerator SwitchScreen(ScreenBase screen, bool skipFadeout)
	{
		if(CurrentScreen != null)
		{
			if (skipFadeout) 
			{
				StopAllCoroutines ();
			}
			else
			{
				yield return StartCoroutine (CurrentScreen.OnScreenFadeOut ());
			}

			CurrentScreen.OnScreenExit ();
			CurrentScreen.gameObject.SetActive (false);
		}

		CurrentScreen = screen;
		CurrentScreen.gameObject.SetActive (true);
		CurrentScreen.OnScreenEnter();

		yield return StartCoroutine (CurrentScreen.OnScreenFadeIn ());

		isSwitching = false;
	}

	private ScreenBase GetScreenByName(string name)
	{
		for(int i = 0; i < screenListLenght; i++)
		{
			if(name == screenList[i].Name)
			{
				return screenList[i];
			}
		}

		Debug.LogError("ScreenName: " + name + " not found!");
		return null;
	}
}
