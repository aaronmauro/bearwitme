using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class SteamAchieve : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized) { return; }
        if (!Input.GetKeyDown(KeyCode.F8)) { return; }
        SteamUserStats.ResetAllStats(true);
        SteamUserStats.StoreStats();
#endif
    }
}