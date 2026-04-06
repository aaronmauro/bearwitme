using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (particles != null)
            {
                particles.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.isPlayer()) return;
        
        if (particles != null)
        {
            particles.Play();
        }
    }
}