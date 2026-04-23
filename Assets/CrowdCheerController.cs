using UnityEngine;

public class CrowdCheerController : MonoBehaviour
{
    public AudioSource cheerSource;
    public float minDelay = 5f;
    public float maxDelay = 15f;

    void Start()
    {
        StartCoroutine(CheerLoop());
    }

    System.Collections.IEnumerator CheerLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            cheerSource.pitch = Random.Range(0.9f, 1.2f);
            cheerSource.Play();
        }
    }
}
