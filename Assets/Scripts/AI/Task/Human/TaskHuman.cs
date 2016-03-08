using System;
using System.Collections;

public abstract class TaskHuman : TaskBase
{
	public sealed override IEnumerator RunTask(AIBase _ai)
	{
		yield return RunTask(_ai as AIHuman);
	}

	public abstract IEnumerator RunTask(AIHuman _ai);
}