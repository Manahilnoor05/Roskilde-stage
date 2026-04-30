using UnityEngine;

public class TeleportToBar : MonoBehaviour
{
    public Transform barPoint;
    
    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        if (barPoint == null)
        {
            Debug.LogWarning("Bar Point missing!");
            return;
        }

        if (cc != null)
            cc.enabled = false;

        transform.position = barPoint.position;
        transform.rotation = barPoint.rotation;

        if (cc != null)
            cc.enabled = true;

        Debug.Log("TELEPORT DONE");
    }
}
