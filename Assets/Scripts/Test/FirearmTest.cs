using System.Collections;
using UnityEngine;

public class FirearmTest : MonoBehaviour
{
	public Firearm weapon;

	protected void OnEnable()
	{
		weapon.onFireEvent += OnFireEvent;
	}

	protected void OnDisable()
	{
		weapon.onFireEvent -= OnFireEvent;
	}

	protected void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			weapon.StartFire();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			weapon.StopFire();
		}
		
		if(Input.GetKeyDown(KeyCode.V))
		{
			weapon.SwitchFireMode();
		}
	}

	private void OnFireEvent(HitInfo hitInfo)
	{
		Debug.Log(hitInfo.Hit ? "Hit: " + hitInfo.Target : "Missed");
	}
}