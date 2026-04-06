using UnityEngine;

public class RiftTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RiftEffectManager.Instance?.ForceOff();

        other.transform.position = teleportTarget.position;
        other.transform.rotation = teleportTarget.rotation;
    }
}