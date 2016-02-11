using System.Collections;
using UnityEngine;

public class FirearmTest : MonoBehaviour
{
	public Firearm weapon;

	void OnEnable()
	{
		weapon.onFireEvent += OnFireEvent;
	}

	void OnDisable()
	{
		weapon.onFireEvent -= OnFireEvent;
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			weapon.StartFire();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			weapon.StopFire();
		}
	}

	private void OnFireEvent(DamageInfo info)
	{
		if(info.Hit)
		{
			Debug.Log("Hit: " + info.Target, info.Target);
		}
		else
		{
			Debug.Log("Missed!");
		}		
	}
}