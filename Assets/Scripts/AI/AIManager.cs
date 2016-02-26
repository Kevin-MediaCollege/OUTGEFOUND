using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour 
{
	public static AIManager instance;
	private List<AIBase> aiList;
	private int aiListLength;

	public Vector3 lastKnownPlayerPosition;

	void Awake()
	{
		instance = this;
		aiList = new List<AIBase>();

		GlobalEvents.AddListener<WeaponFireEvent> (onWeaponFired);
	}

	void Update()
	{
		
	}

	public void onWeaponFired(WeaponFireEvent _event)
	{
		if(_event.Weapon.Entity.HasTag("Player"))
		{
			lastKnownPlayerPosition = _event.Weapon.Entity.gameObject.transform.position;
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

	void OnDestroy()
	{
		GlobalEvents.RemoveListener<WeaponFireEvent> (onWeaponFired);
	}
}
