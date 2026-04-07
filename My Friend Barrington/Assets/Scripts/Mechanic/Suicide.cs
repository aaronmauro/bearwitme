using UnityEngine;
using UnityEngine.InputSystem;

public class Suicide : MonoBehaviour
{
    [Header("Target to Disable")]
    [SerializeField] private GameObject targetObject;

    private bool playerInTrigger = false;

    private void Awake()
    {
        InputManager.GetInstance().submitAction.action.performed += disableCapsule;
        InputManager.GetInstance().submitAction.action.Enable();
    }

    private void OnDisable()
    {
        InputManager.GetInstance().submitAction.action.performed -= disableCapsule;
        InputManager.GetInstance().submitAction.action.Disable();
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void disableCapsule(InputAction.CallbackContext context)
    {
        if (!playerInTrigger) return;
        if (targetObject != null) return;

        targetObject.SetActive(false);
    }
}