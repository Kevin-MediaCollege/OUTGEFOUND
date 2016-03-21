using UnityEngine;
using System.Collections;

public class FirearmSway : MonoBehaviour
{
	private Entity player;

	private Transform weapon;
	public Transform weaponAds;
	private Transform eyes;
	public Transform normalAnim;
	public Transform adsAnim;

	private Curve curveA;
	private float LenghtA;
	private Curve curveB;
	private float LenghtB;
	private float animPos;
	private int curve;
	private Vector3 walkAnim;
	private bool walking;

	private Vector3 delay = new Vector3(0f, 0f, 0f);
	private Vector2 target;
	private Vector2 previous;

	private Vector3 baseNormal = new Vector3(0.15f, 0.59f, 0.55f);
	private Vector3 baseAds = new Vector3(0f, 0.625f, 0.1f);

	void Start () 
	{
		curveA = new Curve(new Vector3(0f, 0f, 0f), new Vector3(-1f, -1f, 0f), new Vector3(-1f, 1f, 0f), new Vector3(0f, 0f, 0f), 100);
		curveB = new Curve(new Vector3(0f, 0f, 0f), new Vector3(1f, -1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(0f, 0f, 0f), 100);
		LenghtA = curveA.lenght;
		LenghtB = curveB.lenght;
		curve = 0;
		animPos = 0f;
		walking = false;
		walkAnim = new Vector3 (0f, 0f, 0f);
	}

	protected void OnEnable()
	{
		walking = false;
		GlobalEvents.AddListener<StartAimDownSightEvent> (OnStartAimDownSight);
		GlobalEvents.AddListener<StopAimDownSightEvent> (OnStopAimDownSight);
		StartCoroutine("Lerp");
	}

	protected void OnDisable()
	{
		walking = false;
		GlobalEvents.RemoveListener<StartAimDownSightEvent> (OnStartAimDownSight);
		GlobalEvents.RemoveListener<StopAimDownSightEvent> (OnStopAimDownSight);
		StopCoroutine("Lerp");
	}

	private void OnStartAimDownSight(StartAimDownSightEvent evt)
	{
		adsAnim.transform.localPosition = baseNormal;
		normalAnim.transform.localPosition = baseAds;
		HOTweenHelper.TransformLocalPosition (adsAnim, baseAds, 0.1f, 0f, Holoville.HOTween.EaseType.Linear);
	}

	private void OnStopAimDownSight(StopAimDownSightEvent evt)
	{
		normalAnim.transform.localPosition = baseAds;
		adsAnim.transform.localPosition = baseNormal;
		HOTweenHelper.TransformLocalPosition (normalAnim, baseNormal, 0.1f, 0f, Holoville.HOTween.EaseType.Linear);
	}

	protected void Update()
	{
		if(player == null || weapon == null)
		{
			player = EntityUtils.GetEntityWithTag("Player");

			if(player == null)
			{
				return;
			}

			weapon = player.GetComponentInChildren<Weapon>().transform.parent;
			eyes = player.transform.Find("Eyes");

			/*
			delay = new Vector3(0f, 0f, 0f);
			previous = new Vector2(eyes.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y);
			target = previous;
			*/
		}

		walking = player.GetComponent<CharacterController> ().velocity != Vector3.zero;
	}

	private IEnumerator	Lerp()
	{
		while(true)
		{
			if (weapon != null) 
			{
				if (walking) 
				{
					animPos += Time.deltaTime * 3f;
					if (curve == 0)
					{
						walkAnim = curveA.getDataAt (animPos * LenghtA).pos;
						if (animPos >= 1f) 
						{
							animPos -= 1f;
							curve = 1;
							walkAnim = curveB.getDataAt (animPos * LenghtB).pos;
						}
					} 
					else 
					{
						walkAnim = curveB.getDataAt (animPos * LenghtB).pos;
						if (animPos >= 1f) 
						{
							animPos -= 1f;
							curve = 0;
							walkAnim = curveA.getDataAt (animPos * LenghtA).pos;
						}
					}
				} 
				else 
				{
					animPos = 0f;
					walkAnim = Vector3.Lerp (walkAnim, Vector3.zero, 0.2f);
				}
	
				weapon.localPosition = (walkAnim * 0.01f) + normalAnim.localPosition;
				weaponAds.localPosition = (walkAnim * 0.002f) + adsAnim.localPosition;
			}

			yield return null;
		}
	}

	/*
	private Vector3 delay = new Vector3(0f, 0f, 0f);
	private Vector2 target;
	private Vector2 previous;

	protected void OnEnable()
	{
		StartCoroutine("Lerp");
	}

	protected void OnDisable()
	{
		StopCoroutine("Lerp");
	}

	protected void Update()
	{
		if(player == null || weapon == null)
		{
			player = EntityUtils.GetEntityWithTag("Player");

			if(player == null)
			{
				return;
			}

			weapon = player.GetComponentInChildren<Weapon>().transform.parent;
			eyes = player.transform.Find("Eyes");

			delay = new Vector3(0f, 0f, 0f);
			previous = new Vector2(eyes.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y);
			target = previous;
		}

		previous = target;
		float targetX = eyes.rotation.eulerAngles.x;
		float targetY = player.transform.rotation.eulerAngles.y;
		target = new Vector2(targetX, targetY);
		delay = Vector3.Slerp (delay, new Vector3 (previous.x - target.x, previous.y - target.y, 0f), 0.05f);
	}

	private IEnumerator	Lerp()
	{
		while(true)
		{
			if(weapon != null)
			{
				//Quaternion newRotation = Quaternion.Euler(delay);
				//weapon.localRotation.eulerAngles = delay;//Quaternion.Slerp(weapon.localRotation, newRotation, (Time.time * 0.1f));

				Quaternion rot = weapon.localRotation;
				Debug.Log (delay.x + "");
				rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z);
				weapon.localRotation = rot;
			}

			yield return null;
		}
	}
	*/
}