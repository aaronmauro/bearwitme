using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{
    public GameObject objectToDestroy; // assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(objectToDestroy);
        }
    }
}