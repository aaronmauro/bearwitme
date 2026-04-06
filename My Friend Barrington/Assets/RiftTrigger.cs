using UnityEngine;

public class RiftTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RiftEffectManager.Instance?.EnterRift();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RiftEffectManager.Instance?.ExitRift();
    }
}