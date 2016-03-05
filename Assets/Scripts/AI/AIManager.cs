using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour 
{
	private static AIManager instance;
	public static AIManager Instance
	{
		get
		{
			return instance;
		}
	}

	private List<AIBase> aiList;
	private int aiListLength;

	public Vector3 lastKnownPlayerPosition;

	protected void Awake()
	{
		instance = this;
		aiList = new List<AIBase>();
	}
	
	protected void OnEnable()
	{
		GlobalEvents.AddListener<FireEvent>(OnWeaponFireEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<FireEvent>(OnWeaponFireEvent);
	}
	public void AddAI(AIBase ai)
	{
		aiList.Add (ai);
		aiListLength++;
	}

	public void RemoveAI(AIBase ai)
	{
		aiList.Remove(ai);
		aiListLength--;
	}

	private void OnWeaponFireEvent(FireEvent evt)
	{
		if(evt.Firearm.Entity.HasTag("Player"))
		{
			lastKnownPlayerPosition = evt.Firearm.Entity.transform.position;
		}
	}
}
