using UnityEngine;

public class BarChoiceInteraction : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;
    public GameObject barText;

    private bool playerAtBar = false;

    void Start()
    {
        if (barText != null)
            barText.SetActive(false);
    }

    void Update()
    {
        if (!playerAtBar || intoxicationSystem == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            intoxicationSystem.currentEffect = IntoxicationSystem.EffectType.Alcohol;
            intoxicationSystem.intensity = 0.4f;

            Debug.Log("Bar: Alcohol chosen");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            intoxicationSystem.currentEffect = IntoxicationSystem.EffectType.Drugs;
            intoxicationSystem.intensity = 0.4f;

            Debug.Log("Bar: Drugs chosen");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtBar = true;

            if (barText != null)
                barText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtBar = false;

            if (barText != null)
                barText.SetActive(false);
        }
    }
}
