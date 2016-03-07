using UnityEngine;
using System.Collections;

public abstract class ScreenBase : MonoBehaviour 
{
	public abstract string Name { get; }

	public virtual void OnScreenEnter()
	{
	}

	public virtual void OnScreenExit()
	{
	}

	public virtual IEnumerator OnScreenFadeIn()
	{
		yield break;
	}

	public virtual IEnumerator OnScreenFadeOut()
	{
		yield break;
	}
}
