using UnityEngine;

public class IntoxicationSystem : MonoBehaviour
{
    public enum EffectType
    {
        None,
        Alcohol,
        Drugs
    }

    [Header("Stage")]
    [Range(0f, 1f)] public float intensity = 0f;
    public EffectType currentEffect = EffectType.None;

    [Header("Test Keys")]
    public float alcoholAmount = 0.35f;
    public float drugAmount = 0.5f;
    public float soberRate = 0.2f;

    void Update()
    {
        if (intensity > 0f)
        {
            intensity -= soberRate * Time.deltaTime;
            intensity = Mathf.Clamp01(intensity);

            if (intensity <= 0.001f)
            {
                intensity = 0f;
                currentEffect = EffectType.None;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddAlcohol(alcoholAmount);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddDrugs(drugAmount);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentEffect = EffectType.Alcohol;
            intensity = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentEffect = EffectType.Drugs;
            intensity = 1f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetEffects();
        }
    }

    public void AddAlcohol(float amount)
    {
        currentEffect = EffectType.Alcohol;
        intensity += amount;
        intensity = Mathf.Clamp01(intensity);
    }

    public void AddDrugs(float amount)
    {
        currentEffect = EffectType.Drugs;
        intensity += amount;
        intensity = Mathf.Clamp01(intensity);
    }

    public void ResetEffects()
    {
        intensity = 0f;
        currentEffect = EffectType.None;
    }
}
  



