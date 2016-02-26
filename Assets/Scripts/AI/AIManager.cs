using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour 
{
	public static AIManager instance;
	private List<AIBase> aiList;
	private int aiListLength;

	void Awake()
	{
		instance = this;
		aiList = new List<AIBase>();
	}

	void Update()
	{
		
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
