using UnityEngine;

public class DangerIndicatorHandler : MonoBehaviour
{

    [SerializeField] Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null) return;
        transform.position = transform.parent.transform.position + new Vector3(0, 3.5f, 0);
        transform.LookAt(cam.transform);
        transform.position += (transform.position - cam.transform.position).normalized * -6f;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20f);
        foreach (var hitCollider in hitColliders)
        {
            //if (hitCollider == null) return;
            //if (hitCollider.gameObject == null) return;
            if (hitCollider.gameObject.GetComponent<SoundWaveStun>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            
        }
    }
}
