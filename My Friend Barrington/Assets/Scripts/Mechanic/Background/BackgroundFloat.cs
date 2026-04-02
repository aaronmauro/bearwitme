using UnityEngine;

public class BackgroundFloat : MonoBehaviour
{
    [SerializeField] private float minHeight = 0.6f;
    [SerializeField] private float maxHeight = 1f;

    [SerializeField] private float minSpeed = 0.75f;
    [SerializeField] private float maxSpeed = 2f;

    private Vector3 startPos;
    private float floatHeight;
    private float floatSpeed;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;

        floatHeight = Random.Range(minHeight, maxHeight);
        floatSpeed = Random.Range(minSpeed, maxSpeed);
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatHeight;
        transform.position = startPos + Vector3.up * y;
    }
}