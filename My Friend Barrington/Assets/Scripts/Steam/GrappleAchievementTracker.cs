using UnityEngine;


#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class GrappleAchievementTracker : MonoBehaviour
{
    private const string AchievementID = "Hooking";
    private const string StatKey = "grapple_count";
    private const int RequiredCount = 20;

    private static int grappleCount = 0;

    private void Start()
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized) return;

        // Load saved count from Steam stats
        SteamUserStats.GetStat(StatKey, out grappleCount);
        Debug.Log($"Grapple count loaded: {grappleCount}");
#else
        // Steam disabled: load from PlayerPrefs so builds can still track progress locally
        grappleCount = PlayerPrefs.GetInt(StatKey, 0);
        Debug.Log($"[DISABLESTEAMWORKS] Grapple count loaded (local): {grappleCount}");
#endif
    }


    public static void RegisterGrapple()
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized) return;

        // Check if already unlocked
        SteamUserStats.GetAchievement(AchievementID, out bool alreadyUnlocked);
        if (alreadyUnlocked) return;

        grappleCount++;
        Debug.Log($"Grapple used! Count: {grappleCount}/{RequiredCount}");

        // Save to Steam
        SteamUserStats.SetStat(StatKey, grappleCount);
        SteamUserStats.StoreStats();

        if (grappleCount >= RequiredCount)
        {
            SteamAchievementManager.UnlockAchievement(AchievementID);
        }
#else
        // Steam disabled: local tracking using PlayerPrefs
        grappleCount++;
        Debug.Log($"[DISABLESTEAMWORKS] Grapple used (local). Count: {grappleCount}/{RequiredCount}");
        PlayerPrefs.SetInt(StatKey, grappleCount);
        PlayerPrefs.Save();

        if (grappleCount >= RequiredCount)
        {
            // Local "unlock" fallback (replace with in-game event if desired)
            Debug.Log($"[DISABLESTEAMWORKS] Achievement unlocked (local): {AchievementID}");
        }
#endif
    }
}