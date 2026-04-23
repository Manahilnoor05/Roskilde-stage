using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualEffectsController : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;
    public Volume volume;

    private ColorAdjustments colorAdjustments;
    private DepthOfField depthOfField;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private Bloom bloom;

    void Start()
    {
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out colorAdjustments);
            volume.profile.TryGet(out depthOfField);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out chromaticAberration);
            volume.profile.TryGet(out lensDistortion);
            volume.profile.TryGet(out bloom);
        }

        if (depthOfField != null)
        {
            depthOfField.mode.value = DepthOfFieldMode.Gaussian;

        }
    }

    void Update()
    {
        if (intoxicationSystem == null)
            return;

        float t = intoxicationSystem.intensity;

        switch (intoxicationSystem.currentEffect)
        {
            case IntoxicationSystem.EffectType.Alcohol:
                ApplyAlcohol(t);
                break;

            case IntoxicationSystem.EffectType.Drugs:
                ApplyDrugs(t);
                break;

            default:
                ApplySober();
                break;
        }

    }

    void ApplySober()
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = 0f;
            colorAdjustments.saturation.value = 0f;
            colorAdjustments.contrast.value = 0f;
            colorAdjustments.hueShift.value = 0f;
        }

        if (depthOfField != null)
        {
            depthOfField.gaussianStart.value = 100f;
            depthOfField.gaussianEnd.value = 200f;
            depthOfField.gaussianMaxRadius.value = 0f;
        }

        if (vignette != null)
        {
            vignette.intensity.value = 0.15f;
            vignette.smoothness.value = 0.2f;
        }

        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = 0f;
        }

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = 0f;
            lensDistortion.scale.value = 1f;
        }

        if (bloom != null)
        {
            bloom.intensity.value = 0.4f;
            bloom.threshold.value = 1f;
        }
    }

    void ApplyAlcohol(float t)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = Mathf.Lerp(0f, -2.8f, t);
            colorAdjustments.saturation.value = Mathf.Lerp(0f, -85f, t);
            colorAdjustments.contrast.value = Mathf.Lerp(0f, 75f, t);
            colorAdjustments.hueShift.value = Mathf.Lerp(0f, 18f, t);
        }

        if (depthOfField != null)
        {
            depthOfField.gaussianStart.value = Mathf.Lerp(25f, 0.05f, t);
            depthOfField.gaussianEnd.value = Mathf.Lerp(60f, 0.25f, t);
            depthOfField.gaussianMaxRadius.value = Mathf.Lerp(0f, 1f, t);
        }

        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(0.15f, 0.55f, t);
            vignette.smoothness.value = Mathf.Lerp(0.2f, 0.95f, t);
        }

        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(0f, 0.75f, t);
        }

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Lerp(0f, -0.35f, t);
            lensDistortion.scale.value = Mathf.Lerp(1f, 0.88f, t);
        }

        if (bloom != null)
        {
            bloom.intensity.value = Mathf.Lerp(0.4f, 2.5f, t);
            bloom.threshold.value = Mathf.Lerp(1f, 0.4f, t);
        }
    }

    void ApplyDrugs(float t)
    {
        float pulse = (Mathf.Sin(Time.time * 4f) + 1f) * 0.5f;

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = Mathf.Lerp(0f, 1.5f, t);
            colorAdjustments.saturation.value = Mathf.Lerp(0f, 100f, t);
            colorAdjustments.contrast.value = Mathf.Lerp(0f, 100f, t);
            colorAdjustments.hueShift.value = Mathf.Lerp(0f, 120f + 80f * pulse, t);
        }

        if (depthOfField != null)
        {
            depthOfField.gaussianStart.value = Mathf.Lerp(20f, 0.01f, t);
            depthOfField.gaussianEnd.value = Mathf.Lerp(40f, 0.08f, t);
            depthOfField.gaussianMaxRadius.value = Mathf.Lerp(0f, 1f, t);
        }

        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(0.1f, 0.45f, t);
            vignette.smoothness.value = Mathf.Lerp(0.2f, 1f, t);
        }

        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, t);
        }

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Lerp(0f, Mathf.Lerp(-0.6f, 0.6f, pulse), t);
            lensDistortion.scale.value = Mathf.Lerp(1f, 0.8f + 0.2f * pulse, t);
        }

        if (bloom != null)
        {
            bloom.intensity.value = Mathf.Lerp(0.5f, 5f + 3f * pulse, t);
            bloom.threshold.value = Mathf.Lerp(1f, 0.1f, t);
        }


    }

} 