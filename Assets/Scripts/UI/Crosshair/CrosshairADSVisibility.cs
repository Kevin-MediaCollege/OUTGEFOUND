using UnityEngine;
using UnityEngine.UI;

public class CrosshairADSVisibility : MonoBehaviour
{
	private Image[] visibilityTargets;
	private bool visible;

	protected void Awake()
	{
		visibilityTargets = GetComponentsInChildren<Image>();
		visible = true;
	}

	protected void OnEnable()
	{
		GlobalEvents.AddListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		GlobalEvents.AddListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<StartAimDownSightEvent>(OnStartAimDownSightEvent);
		GlobalEvents.RemoveListener<StopAimDownSightEvent>(OnStopAimDownSightEvent);
	}

	protected void LateUpdate()
	{
		foreach(Image image in visibilityTargets)
		{
			image.enabled = visible;
		}
	}

	private void OnStartAimDownSightEvent(StartAimDownSightEvent evt)
	{
		visible = false;
	}

	private void OnStopAimDownSightEvent(StopAimDownSightEvent evt)
	{
		visible = true;
	}
}