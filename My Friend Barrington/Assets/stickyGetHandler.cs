using UnityEngine;

public class stickyGetHandler : MonoBehaviour
{
    [SerializeField] Transform oldPos;
    [SerializeField] Transform targetPos;
    bool offWhenDone;
    [SerializeField] float journeyTime = 1f;
    float startTime;
    [SerializeField] Transform playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - startTime) / journeyTime > 1)
        {
            if (offWhenDone)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;
        }
        transform.position = Vector3.Slerp(oldPos.position, targetPos.position, (Time.time - startTime) / journeyTime);
        transform.localScale = Vector3.Slerp(oldPos.localScale, targetPos.localScale, (Time.time - startTime) / journeyTime);
    }

    public void StickyGive()
    {
        transform.position = transform.parent.transform.position;
        transform.localScale = Vector3.zero;
        oldPos = transform;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        targetPos.position = transform.position + new Vector3(0, 7, 0);
        targetPos.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        startTime = Time.time;
    }

    public void StickyGet()
    {
        oldPos = transform;
        targetPos.position = playerPos.position;
        targetPos.localScale = Vector3.zero;
        offWhenDone = true;
        startTime = Time.time;
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
