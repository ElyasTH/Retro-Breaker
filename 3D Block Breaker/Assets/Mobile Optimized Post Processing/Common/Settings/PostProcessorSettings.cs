using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Settings for PostProcessor
/// </summary>
[CreateAssetMenu(fileName = "PostProcessorSettings", menuName = "Mobile Optimized Post Processing/PostProcessorSettings", order = 0)]
public class PostProcessorSettings : ScriptableObject {
    [Space]
	
	[Header("Vignette settings")]
	[Tooltip("Is vignette effect enabled?")]
	public bool VignetteEnabled = false;
	
	[Tooltip("Intensity of vignette effect, this value does not affect performance")] [Range(0, 5)] public float VignettePower = 0.5f;
	[Tooltip("Vignette center relative to center of the screen, this value does not affect performance")] public Vector2 VignetteCenter = new Vector2(0.0f, 0.0f);

    [Space]

	[Header("Screen blur settings")]
	[Tooltip("Is blur effect enabled")] public bool BlurEnabled = false;
	[Tooltip("Radius of blur effect, this value does not affect performance, if radius is equal to 0 then blur effect is skipped in rendering process")] [Range(0, .006f)] public float BlurRadius = 0.0012f;
	[Tooltip("Value by which resoultion will be divided while applying blur effect, try to keep this value as integer for the best visuals, keep this value as high as possible for best performance")] [Range(1, 10)] public float BlurTextureResolutionDivider = 8;
	[Tooltip("Amount of times blur effect will be applied to final image, keep it as low as possible for best performance for nice blur effect keep it high with low BlurTextureResolutionDivider parameter value")] [Range(1, 10)] public int BlurIterations = 6;

	[Space]

	[Header("Enable color grading effects")]
	[Tooltip("Are color grading effects enabled? (tone mapping, LUT, chromatic abberation")]
	public bool ColorGradingEnabled = false;

	[Space]

	[Header("Tone mapping settings")]

	[Tooltip("Is tone mapping enabled")] public bool ToneMappingEnabled = false;
	[Tooltip("Gamma value, this value does not affect performance")] [Range(0, 10)] public float Gamma = 1.1f;
	[Tooltip("Exposure value, this value does not affect performance")] [Range(1, 10)] public float Exposure = 1.8f;

	[Space]

	[Header("Look up table settings")]
	[Tooltip("Look up table effect enabled?")]
	public bool LUTEnabled = false;
	[Tooltip("LUT texture, remember to properly set your LUT texture import settings (disabled mipmapping, compression etc)! Use included sample LUT textures as reference.")] public Texture LUT;
	[Tooltip("LUT effect intensity")] [Range(0, 1)] public float LUTIntensity = 0f;
	[Tooltip("LUT texture colors amount (every included LUT texture have 32 colors)")] public int ColorsAmount = 32;

	[Space]

	[Header("Chromatic abberation settings")]

	[Tooltip("Chromatic abberation enabled?")]
	public bool ChromaticAbberationEnabled = false;
	[Tooltip("How much each color channel will be shifted")] [Range(0, 0.03f)] public float ColorsShiftAmount = 0.001f;
	[Tooltip("If this parameter value is 0 then amount of color shift is the same on the whole area of the screen, if this parameter value is 1 then colors are shifted by amount based on the distance from current pixel to the center of the screen (pixels at center are not shifted at all, pixels at screen sides are shifted by ColorsShiftAmount value")] [Range(0, 1f)] public float FishEyeEffectFactor = 1f;
	[Tooltip("Have effect only if FishEyeEffectFactor > 0.0, distance from center of screen where fish eye effect starts to appear (0 - center of the screen, 1 - side of the screen")] [Range(0.0f, 1.0f)] public float FishEyeEffectStart = 0.3f;
	[Tooltip("Have effect only if FishEyeEffectFactor > 0.0, distance from center of screen where fish eye effect ends (0 - center of the screen, 1 - side of the screen")] [Range(0.0f, 1.0f)] public float FishEyeEffectEnd = 1.0f;

	[Space]

	[Header("Bloom settings")]
	[Tooltip("Is bloom effect enabled?")]
	public bool BloomEnabled = false;
	[Space]
	[Tooltip("Radius of blur applied to bloom texture, this value does not affect performance")] [Range(0, .006f)] public float BloomBlurRadius = 0.002f;
	[Tooltip("Value by which resoultion will be divided while applying blur effect to bloom texture, try to keep this value as integer for the best visuals, keep this value as high as possible for best performance")] [Range(1, 10)] public float BloomBlurTextureResolutionDivider = 10;
	[Tooltip("Amount of times blur effect will be applied to bloom texture, keep it as low as possible for best performance for nice bloom effect keep it high with low BlurTextureResolutionDivider parameter value")] [Range(1, 10)] public int BloomBlurIterations = 3;
	[Tooltip("Value by which resoultion will be divided while applying brightness filter shader to screen texture, you probably want this value to be 1 all the time because it saves only a little bit of rendering time and makes bloom effect a lot uglier, try to keep this value as integer for the best visuals")] [Range(1, 10)] public float FilterTextureResolutionDivider = 1;

	[Space]
	
	[Tooltip("Intensity of bloom effect, this value does not affect performance")] [Range(0, 8)] public float BloomIntensity = 0.35f;
	[Tooltip("Minimum brigtness of pixel to be considered as glowing one, this value does not affect performance")] [Range(0, 10)] public float BloomThreshold = 0f;
	[Tooltip("When value is 0 pixels with brightness value under BloomThreshold are just cutoff, when Soft Threshold equals 1 then pixels with brightness value between 0 and BloomThreshold are nicely faded out")] [Range(0, 10)] public float BloomSoftThreshold = 0f;

	[Tooltip("With this option enabled you can see filtered and blurred scene texture with only glowing parts of scene")] public bool BloomDebugView = false;
}

