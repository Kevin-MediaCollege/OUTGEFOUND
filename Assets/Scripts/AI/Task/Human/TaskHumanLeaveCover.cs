using UnityEngine;
using System.Collections;

public class TaskHumanLeaveCover : TaskHuman
{
	public override IEnumerator RunTask(AIHuman _ai)
	{
		_ai.CurrentCover = null;
		crouch (false, _ai);

		yield return new WaitForSeconds(0.5f);
	}

	public void crouch(bool _state, AIHuman _ai)
	{
		if (_state) 
		{
			_ai.Movement.Crouching = true;
			_ai.Movement.Agent.height = 1.5f;
			_ai.Movement.Agent.baseOffset = 0.4f;
		} 
		else 
		{
			_ai.Movement.Crouching = false;
			_ai.Movement.Agent.height = 2f;
			_ai.Movement.Agent.baseOffset = 1f;
		}
	}
}