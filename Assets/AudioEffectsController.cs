using UnityEngine;

public class AudioEffectsController : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;

    [Header("Audio Sources")]
    public AudioSource stageMusic;
    public AudioSource crowdAmbience;
    public AudioSource crowdCheering;

    [Header("Filters")]
    public AudioLowPassFilter stageLowPass;
    public AudioLowPassFilter ambienceLowPass;
    public AudioLowPassFilter cheeringLowPass;

    public AudioReverbFilter stageReverb;
    public AudioReverbFilter ambienceReverb;
    public AudioReverbFilter cheeringReverb;

    void Update()
    {
        if (intoxicationSystem == null)
            return;

        float t = intoxicationSystem.intensity;

        switch (intoxicationSystem.currentEffect)
        {
            case IntoxicationSystem.EffectType.Alcohol:
                ApplyAlcoholAudio(t);
                break;

            case IntoxicationSystem.EffectType.Drugs:
                ApplyDrugAudio(t);
                break;

            default:
                ApplySoberAudio();
                break;
        }
    }

    void ApplySoberAudio()
    {
        // Normal lyd
        SetLowPass(stageLowPass, 22000f);
        SetLowPass(ambienceLowPass, 22000f);
        SetLowPass(cheeringLowPass, 22000f);

        SetPitch(stageMusic, 1f);
        SetPitch(crowdAmbience, 1f);
        SetPitch(crowdCheering, 1f);

        SetReverb(stageReverb, 0f, 0f);
        SetReverb(ambienceReverb, 0f, 0f);
        SetReverb(cheeringReverb, 0f, 0f);
    }

    void ApplyAlcoholAudio(float t)
    {
        // Lyden bliver dæmpet og tung 
        float cutoff = Mathf.Lerp(22000f, 900f, t);

        SetLowPass(stageLowPass, cutoff);
        SetLowPass(ambienceLowPass, Mathf.Lerp(22000f, 1200f, t));
        SetLowPass(cheeringLowPass, Mathf.Lerp(22000f, 1500f, t));

        // Lidt lavere og tungere pitch
        SetPitch(stageMusic, Mathf.Lerp(1f, 0.82f, t));
        SetPitch(crowdAmbience, Mathf.Lerp(1f, 0.88f, t));
        SetPitch(crowdCheering, Mathf.Lerp(1, 0.9f, t));

        // Mere rumfølelse/slør
        SetReverb(stageReverb, Mathf.Lerp(0f, -1000f, t), Mathf.Lerp(0f, 1000f, t));
        SetReverb(ambienceReverb, Mathf.Lerp(0f, -1200f, t), Mathf.Lerp(0f, 800f, t));
        SetReverb(cheeringReverb, Mathf.Lerp(0f, -800f, t), Mathf.Lerp(0f, 1200f, t));
    }

    void ApplyDrugAudio(float t)
    {
        float pulse = (Mathf.Sin(Time.time * 4f) + 1f) * 0.5f;

        //Mærkeligere lyd: cutoff pulserer
        SetLowPass(stageLowPass, Mathf.Lerp(22000f, 600f + 3000f * pulse, t));
        SetLowPass(ambienceLowPass, Mathf.Lerp(22000f, 800f + 2000f * pulse, t));
        SetLowPass(cheeringLowPass, Mathf.Lerp(22000f, 1000f + 2500f * pulse, t));

        // Pitch pulserer op og ned 
        SetPitch(stageMusic, Mathf.Lerp(1f, 0.7f + 0.6f * pulse, t));
        SetPitch(crowdAmbience, Mathf.Lerp(1f, 0.75f + 0.5f * pulse, t));
        SetPitch(crowdCheering, Mathf.Lerp(1f, 0.8f + 0.5f * pulse, t));

        // Mere psykedelisk rumklang 
        SetReverb(stageReverb, Mathf.Lerp(0f, -200f, t), Mathf.Lerp(0f, 2000f, t));
        SetReverb(ambienceReverb, Mathf.Lerp(0f, -300f, t), Mathf.Lerp(0f, 1800f, t));
        SetReverb(cheeringReverb, Mathf.Lerp(0f, -100f, t), Mathf.Lerp(0f, 2200f, t));
    }

    void SetLowPass(AudioLowPassFilter filter, float cutoff)
    {
        if (filter != null)
        {
            filter.cutoffFrequency = cutoff;
        }
    }

    void SetPitch(AudioSource source, float pitch)
    {
        if (source != null)
        {
            source.pitch = pitch;
        }
    }

    void SetReverb(AudioReverbFilter reverb, float dryLevel, float room)
    {
        if (reverb != null)
        {
            reverb.dryLevel = dryLevel;
            reverb.room = room;
        }
    }
}
