using UnityEngine;
using FMODUnity;
public class CloudTriggerSound : MonoBehaviour
{
    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference cloudSoundEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RuntimeManager.PlayOneShotAttached(cloudSoundEvent, gameObject);
        }
    }
}