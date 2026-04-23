using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;
    public PlayerMovement playerMovement;
    public Transform cameraTransform;
    private MouseLook mouseLook;

    [Header("Movement")]
    public float normalSpeed = 2.5f;
    public float alcoholSpeed = 0.1f;
    public float drugSpeed = 0.8f;

    [Header("Shake")]
    public float alcoholShakeAmount = 0.28f;
    public float alcoholShakeSpeed = 10f;

    public float drugShakeAmount = 0.4f;
    public float drugShakeSpeed = 18f;

    [Header("Tilt")]
    public float alcoholTiltAmount = 45f;
    public float alcoholTiltSpeed = 3f;

    public float drugTiltAmount = 70f;
    public float drugTiltSpeed = 8f;

    [Header("Yaw drift")]
    public float alcoholYawDrift = 20f;
    public float drugYawDrift = 18f;

    private Vector3 originalCameraLocalPos;
    private Quaternion originalCameraLocalRot;

    void Start()
    {
        if (cameraTransform != null)
        {
            originalCameraLocalPos = cameraTransform.localPosition;
            originalCameraLocalRot = cameraTransform.localRotation;
        }

        mouseLook = cameraTransform.GetComponent<MouseLook>();
    }

    void Update()
    {
        if (intoxicationSystem == null || cameraTransform == null)
            return;

        float t = intoxicationSystem.intensity;

        if (t > 0.1f)
        {
            mouseLook.allowLook = false;
        }
        else
        {
            mouseLook.allowLook = true;
        }

        switch (intoxicationSystem.currentEffect)
            {
                case IntoxicationSystem.EffectType.Alcohol:
                    ApplyAlcoholMovement(t);
                    ApplyAlcoholCamera(t);
                    break;

                case IntoxicationSystem.EffectType.Drugs:
                    ApplyDrugMovement(t);
                    ApplyDrugCamera(t);
                    break;

                default:
                    ApplySober();
                    break;
            }
    }

    void ApplySober()
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed = normalSpeed;
        }

        cameraTransform.localPosition = Vector3.Lerp(
            cameraTransform.localPosition,
            originalCameraLocalPos,
            Time.deltaTime * 8f
        );

        cameraTransform.localRotation = Quaternion.Lerp(
            cameraTransform.localRotation,
            originalCameraLocalRot,
            Time.deltaTime * 8f
        );
    }

    void ApplyAlcoholMovement(float t)
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed = Mathf.Lerp(normalSpeed, alcoholSpeed, t);
        }
    }

    void ApplyDrugMovement(float t)
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed = Mathf.Lerp(normalSpeed, drugSpeed, t);
        }
    }

    void ApplyAlcoholCamera(float t)
    {
        float time = Time.time;

        float tiltZ = Mathf.Sin(time * 2.5f) * 70f * t;
        float yawY = Mathf.Sin(time * 1.5f) * 25f * t;
        float pitchX = Mathf.Cos(time * 1.2f) * 10f * t;

        Quaternion targetRot = Quaternion.Euler(pitchX, yawY, tiltZ);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRot,
            Time.deltaTime * 5f
        );
    }

    void ApplyDrugCamera(float t)
    {
        float time = Time.time;

        float pulse = Mathf.Sin(time * 6f);

        float tiltZ = Mathf.Sin(time * 5f) * 110f * t;
        float yawY = Mathf.Sin(time * 3f) * 45f * t;
        float pitchX = Mathf.Cos(time * 4f) * 25f * t;

        Quaternion targetRot = Quaternion.Euler(pitchX, yawY, tiltZ);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRot,
            Time.deltaTime * 8f
        );
    }
}
