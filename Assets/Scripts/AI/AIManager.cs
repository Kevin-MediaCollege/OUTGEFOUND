using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour 
{
	public static AIManager instance;

	//List
	private List<AIBase> aiList;
	private int aiListLength;

	//Logic
	private int nextLogicUpdate;
	private int logicUpdatesPerFrame = 20;

	void Awake()
	{
		instance = this;
		aiList = new List<AIBase>();
	}

	void Update()
	{
		updateLogic();
	}

	private void updateLogic()
	{
		nextLogicUpdate = nextLogicUpdate >= aiListLength ? 0 : nextLogicUpdate;
		int end = nextLogicUpdate + logicUpdatesPerFrame;
		end = end >= aiListLength ? aiListLength : end;

		for(int i = nextLogicUpdate; i < end; i++)
		{
			//aiList[i].onLogicUpdate();
			nextLogicUpdate++;
		}
	}

	public void addAI(AIBase _ai)
	{
		aiList.Add (_ai);
		aiListLength++;
	}

	public void removeAI(AIBase _ai)
	{
		aiList.Remove (_ai);
		aiListLength--;
	}
}
