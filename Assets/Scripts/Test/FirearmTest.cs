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
			weapon.TryFire();
		}
	}

	private void OnFireEvent(HitInfo hitInfo)
	{
		Debug.Log("Hit: " + hitInfo.target, hitInfo.target);
	}
}