using UnityEngine;

public class GameSettingsLoader : MonoBehaviour
{
	[SerializeField] private GameCamera gameCamera;
	[SerializeField] private PlayerInputController playerInputController;

	protected void Awake()
	{
		ApplySettings();
	}
	
	protected void OnEnable()
	{
		GlobalEvents.AddListener<ReloadSettingsEvent>(OnReloadSettingsEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<ReloadSettingsEvent>(OnReloadSettingsEvent);
	}

	private void ApplySettings()
	{
		gameCamera.Sensitivity = new Vector2(GameSettings.MouseSensitivityX, GameSettings.MouseSensitivityY);
		playerInputController.ToggleADS = GameSettings.ToggleADS;
		playerInputController.ToggleCrouch = GameSettings.ToggleCrouch;
	}

	private void OnReloadSettingsEvent(ReloadSettingsEvent evt)
	{
		ApplySettings();
	}
}
