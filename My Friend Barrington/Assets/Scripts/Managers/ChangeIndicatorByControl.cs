using System.Data.Common;
using UnityEngine;

public class ChangeIndicatorByControl : MonoBehaviour
{
    [SerializeField] Sprite[] indicators;
    [SerializeField] SpriteRenderer currentSprite;
    //[SerializeField] bool leverControler, grappleControler, dialougeControler;
    public enum pickSprite { lever, grapple, dialouge }
    public pickSprite materialType;
    [SerializeField] bool controlerActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeIndicator();
        Debug.Log(!InputManager.controlerUI);
    }

    public void ChangeIndicator()
    {
        if (currentSprite == null)
            currentSprite = GetComponent<SpriteRenderer>();
        //controlerActive = !InputManager.controlerUI;
        currentSprite.sprite = materialType switch
        {
            pickSprite.lever => !InputManager.controlerUI ? indicators[0] : indicators[1],
            pickSprite.grapple => !InputManager.controlerUI ? indicators[2] : indicators[3],
            pickSprite.dialouge => !InputManager.controlerUI ? indicators[4] : indicators[5],
            _ => currentSprite.sprite
        };
    }
}
