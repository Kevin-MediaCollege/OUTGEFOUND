using System;
using System.Collections;
using UnityEngine;

public class GraphicsSettings : ScriptableObjectSingleton<GraphicsSettings>
{
	public enum TextureQualityLevel
	{
		LOW = 2,
		MEDIUM = 1,
		HIGH = 0
	}

	public enum AntiAliasingLevel
	{
		NONE = -1,
		FXAA = 0,
		SSAA = 5,
		DLAA = 6
	}

	public static TextureQualityLevel TextureQuality { set; get; }

	public static bool AnisotropicFiltering { set; get; }

	public static bool RealtimeReflections { set; get; }

	public static bool LensDirt { set; get; }

	public static bool ColorCorrection { set; get; }

	public static bool Bloom { set; get; }

	public static bool VingetteChromaticAberration { set; get; }

	public static bool SSAO { set; get; }

	public static bool TiltShift { set; get; }

	public static AntiAliasingLevel AntiAliasing { set; get; }

	public static void ApplyModifiedChanges()
	{
		PlayerPrefs.SetInt("SETTING_GRAPHICS_TEXTURE_QUALITY", (int)TextureQuality);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_ANISOTROPIC_FILTERING", AnisotropicFiltering ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_REALTIME_REFLECTIONS", RealtimeReflections ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_LENS_DIRT", LensDirt ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_COLOR_CORRECTION", ColorCorrection ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_BLOOM", Bloom ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_VINGETTE_CHROMATIC_ABERRATION", VingetteChromaticAberration ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_SSAO", SSAO ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_TILT_SHIFT", TiltShift ? 1 : 0);
		PlayerPrefs.SetInt("SETTING_GRAPHICS_ANTI_ALIASING", (int)AntiAliasing);
	}

	public static void Load()
	{
		TextureQuality = (TextureQualityLevel)PlayerPrefs.GetInt("SETTING_GRAPHICS_TEXTURE_QUALITY", (int)Instance.defaultTextureQuality);
		AnisotropicFiltering = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_ANISOTROPIC_FILTERING", Convert.ToInt32(Instance.defaultAnisotropicFiltering)));
		RealtimeReflections = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_REALTIME_REFLECTIONS", Convert.ToInt32(Instance.defaultRealtimeReflections)));
		LensDirt = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_LENS_DIRT", Convert.ToInt32(Instance.defaultLensDirt)));
		ColorCorrection = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_COLOR_CORRECTION", Convert.ToInt32(Instance.defaultColorCorrection)));
		Bloom = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_BLOOM", Convert.ToInt32(Instance.defaultBloom)));
		VingetteChromaticAberration = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_VINGETTE_CHROMATIC_ABERRATION", Convert.ToInt32(Instance.defaultVingetteChromaticAberration)));
		SSAO = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_SSAO", Convert.ToInt32(Instance.defaultSSAO)));
		TiltShift = Convert.ToBoolean(PlayerPrefs.GetInt("SETTING_GRAPHICS_TILT_SHIFT", Convert.ToInt32(Instance.defaultTiltShift)));
		AntiAliasing = (AntiAliasingLevel)PlayerPrefs.GetInt("SETTING_GRAPHICS_ANTI_ALIASING", (int)Instance.defaultAntiAliasing);
	}

	public static void Cancel()
	{
		Load();
	}

	[SerializeField] private TextureQualityLevel defaultTextureQuality;
	[SerializeField] private bool defaultAnisotropicFiltering;
	[SerializeField] private bool defaultRealtimeReflections;
	[SerializeField] private bool defaultLensDirt;
	[SerializeField] private bool defaultColorCorrection;
	[SerializeField] private bool defaultBloom;
	[SerializeField] private bool defaultVingetteChromaticAberration;
	[SerializeField] private bool defaultSSAO;
	[SerializeField] private bool defaultTiltShift;
	[SerializeField] private AntiAliasingLevel defaultAntiAliasing;

	protected void OnEnable()
	{
		Load();
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Settings/Graphics")]
	private static void Create()
	{
		CreateAsset("Create GraphicsSettings", "GraphicsSettings", "Create GraphicsSettings");
	}
#endif
}