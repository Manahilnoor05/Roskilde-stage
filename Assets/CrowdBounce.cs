using UnityEngine;

public class CrowdBounce : MonoBehaviour
{
    public float bounceHeight = 0.2f;
    public float bounceSpeed = 2f;

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin((Time.time + randomOffset) * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
