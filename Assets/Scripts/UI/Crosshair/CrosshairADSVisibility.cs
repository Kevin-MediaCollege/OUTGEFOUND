using UnityEngine;
using UnityEngine.UI;

public class CrosshairADSVisibility : MonoBehaviour
{
	private Image[] visibilityTargets;

	private AimDownSightController adsController;

	protected void Awake()
	{
		visibilityTargets = GetComponentsInChildren<Image>();

		adsController = Dependency.Get<AimDownSightController>();
	}

	protected void LateUpdate()
	{
		foreach(Image image in visibilityTargets)
		{
			image.enabled = !adsController.IsAimingDownSight;
		}
	}
}