using System.Collections;
using UnityEngine;

/// <summary>
/// A simple condition which waits for the specified amount of seconds to be over before returning true.
/// </summary>
public class TimedCondition : LinkCondition
{
	[SerializeField] private float delay;

	private bool isValid;
	public override bool IsValid
	{
		get
		{
			return isValid;
		}
	}

	public override void Create()
	{
		isValid = false;

		Dependency.Get<CoroutineRunner>().StartCoroutine(Delay(delay));
	}

	/// <summary>
	/// Delay coroutine.
	/// </summary>
	/// <param name="delay">The time to delay, in seconds.</param>
	private IEnumerator Delay(float delay)
	{
		yield return new WaitForSeconds(delay);

		isValid = true;
	}
}