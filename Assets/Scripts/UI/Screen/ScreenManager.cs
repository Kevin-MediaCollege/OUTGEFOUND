using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour 
{
	public static ScreenManager Instance;

	public ScreenBase[] screenList;
	private int screenListLenght;
	private bool isSwitching;
	public string startScreen;
	public ScreenBase currentScreen {
		private set;
		get;
	}

	void Awake()
	{
		Instance = this;
		isSwitching = false;

		screenListLenght = screenList.Length;
		for(int i = 0; i < screenListLenght; i++)
		{
			screenList [i].gameObject.SetActive (false);
		}

		setScreen (startScreen);
	}

	public void setScreen(string _name)
	{
		if(!isSwitching)
		{
			ScreenBase screen = getScreenByName (_name);
			if(screen != null)
			{
				isSwitching = true;
				StartCoroutine (switchScreen (screen));
			}
		}
	}

	private IEnumerator switchScreen(ScreenBase _screen)
	{
		if(currentScreen != null)
		{
			yield return StartCoroutine (currentScreen.OnScreenFadeout ());
			currentScreen.OnScreenExit ();
			currentScreen.gameObject.SetActive (false);
		}

		currentScreen = _screen;
		currentScreen.gameObject.SetActive (true);
		currentScreen.OnScreenEnter();
		yield return StartCoroutine (currentScreen.OnScreenFadein ());
		isSwitching = false;
	}

	private ScreenBase getScreenByName(string _name)
	{
		for(int i = 0; i < screenListLenght; i++)
		{
			if(_name == screenList[i].getScreenName())
			{
				return screenList[i];
			}
		}
		Debug.LogError ("ScreenName: " + _name + " not found!");
		return null;
	}
}
