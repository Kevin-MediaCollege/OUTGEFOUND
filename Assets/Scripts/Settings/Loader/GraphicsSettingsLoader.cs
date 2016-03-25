using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class GraphicsSettingsLoader : MonoBehaviour
{
	[SerializeField] private SENaturalBloomAndDirtyLens lensDirt;
	[SerializeField] private ColorCorrectionLookup colorCorrection;
	[SerializeField] private Bloom bloom;
	[SerializeField] private VignetteAndChromaticAberration vingetteChromaticAberration;
	[SerializeField] private ScreenSpaceAmbientOcclusion ssao;
	[SerializeField] private PostEffectsBase tiltShift;
	[SerializeField] private Antialiasing antiAliasing;

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
		QualitySettings.masterTextureLimit = (int)GraphicsSettings.TextureQuality;
		QualitySettings.anisotropicFiltering = GraphicsSettings.AnisotropicFiltering ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;
		QualitySettings.realtimeReflectionProbes = GraphicsSettings.RealtimeReflections;

		lensDirt.enabled = GraphicsSettings.LensDirt;
		colorCorrection.enabled = GraphicsSettings.ColorCorrection;
		bloom.enabled = GraphicsSettings.Bloom;
		vingetteChromaticAberration.enabled = GraphicsSettings.VingetteChromaticAberration;
		ssao.enabled = GraphicsSettings.SSAO;
		tiltShift.enabled = GraphicsSettings.TiltShift;
		antiAliasing.mode = (AAMode)((int)GraphicsSettings.AntiAliasing);
	}

	private void OnReloadSettingsEvent(ReloadSettingsEvent evt)
	{
		ApplySettings();
	}
}
