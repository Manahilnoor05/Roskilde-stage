using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public IntoxicationSystem intoxicationSystem;
    public float moveSpeed;
    public float normalSpeed = 2.5f;
    public float alcoholSpeed = 0.1f;
    public float drugSpeed = 0.8f; 
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = normalSpeed;

        if (intoxicationSystem != null)
        {
            float t = intoxicationSystem.intensity;
            if (intoxicationSystem.currentEffect == IntoxicationSystem.EffectType.Alcohol)
            {
                currentSpeed = Mathf.Lerp(normalSpeed, alcoholSpeed, t);
            }
            else if (intoxicationSystem.currentEffect == IntoxicationSystem.EffectType.Drugs)
            {
                currentSpeed = Mathf.Lerp(normalSpeed, drugSpeed, t);
            }
        }

        moveSpeed = currentSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}