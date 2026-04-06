using System.Collections.Generic;
using UnityEngine;

public class FianlPeteSpeech : MonoBehaviour
{

    private bool isRiftOpen = false;
    private bool isRiftClose = false;
    [SerializeField] private TextAsset inkJSON;
    private DialogueTrigger dT;
    [SerializeField] private List<GameObject> rifts = new List<GameObject>();

    // ensure we only swap the ink JSON once
    private bool hasSetInkJSON = false;

    void Start()
    {
        dT = GetComponent<DialogueTrigger>();

        // If all rifts are already closed at start, apply immediately
        TryApplyInkIfAllRiftsClosed();
    }

    void Update()
    {
        // only check while we haven't applied the new ink JSON yet
        if (hasSetInkJSON) return;

        // keep the original intent of checking whether a portal opened at least once
        if (!isRiftOpen)
        {
            CheckPortalOpen();
            return;
        }

        // Check whether all rifts are closed
        TryApplyInkIfAllRiftsClosed();
    }

    private void TryApplyInkIfAllRiftsClosed()
    {
        if (rifts == null || rifts.Count == 0) return;

        // If any rift is active, we are not fully closed
        bool anyOpen = false;
        foreach (GameObject rift in rifts)
        {
            if (rift != null && rift.activeInHierarchy)
            {
                anyOpen = true;
                break;
            }
        }

        isRiftClose = !anyOpen;

        if (isRiftClose && !hasSetInkJSON)
        {
            if (dT != null)
            {
                dT.inkJSON = inkJSON;
                hasSetInkJSON = true;
            }
        }
    }

    private void CheckPortalOpen()
    {
        // mark that a rift has been open (keeps original behavior but more robust)
        for (int i = 0; i < rifts.Count; i++)
        {
            if (rifts[i] != null && rifts[i].activeInHierarchy)
            {
                isRiftOpen = true;
                return;
            }
        }
    }
}
