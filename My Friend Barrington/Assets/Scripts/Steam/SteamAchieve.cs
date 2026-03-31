using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class SteamAchieve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F8)) { return; }

#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized)
        {
            Debug.LogWarning("[SteamAchieve] SteamManager not initialized. Cannot reset Steam stats.");
            return;
        }

        // use to reset stats when testing
        SteamUserStats.ResetAllStats(true);
        SteamUserStats.StoreStats();
        Debug.Log("[SteamAchieve] Steam stats reset and stored.");
#else
        // Local fallback for non‑Steam builds:
        // - Clear grapple_count used by GrappleAchievementTracker.
        // - If you want to wipe all local data during testing, uncomment PlayerPrefs.DeleteAll().
        PlayerPrefs.DeleteKey("grapple_count");

        // Uncomment to clear all PlayerPrefs during testing (be careful in production):
        // PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();
        Debug.Log("[DISABLESTEAMWORKS] Local test stats reset (grapple_count cleared).");
#endif
    }
}
