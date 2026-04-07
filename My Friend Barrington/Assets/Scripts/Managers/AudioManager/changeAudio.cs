using FMODUnity;
using UnityEngine;

public class changeAudio : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter audioEmitter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioEmitter = GetComponent<StudioEventEmitter>();
    }

    private void OnDisable()
    {
        audioEmitter.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            audioEmitter.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            audioEmitter.Stop();
        }
    }
}
