using UnityEngine;


#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class SteamAchievementManager : MonoBehaviour
{
    public static void UnlockAchievement(string achievementID)
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized)
        {
            Debug.LogWarning("Steam not initialized. Cannot unlock achievement.");
            return;
        }

        bool alreadyUnlocked;
        SteamUserStats.GetAchievement(achievementID, out alreadyUnlocked);

        if (!alreadyUnlocked)
        {
            SteamUserStats.SetAchievement(achievementID);
            SteamUserStats.StoreStats();
            Debug.Log($"Achievement unlocked: {achievementID}");
        }
#else
        // Local fallback for non‑Steam builds: persist unlocked state in PlayerPrefs
        string key = $"achieve_{achievementID}_unlocked";
        if (PlayerPrefs.GetInt(key, 0) == 1) return;

        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        Debug.Log($"[DISABLESTEAMWORKS] Locally unlocked achievement: {achievementID}");
#endif
    }

    public static bool IsAchievementUnlocked(string achievementID)
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized) return false;
        SteamUserStats.GetAchievement(achievementID, out bool unlocked);
        return unlocked;
#else
        return PlayerPrefs.GetInt($"achieve_{achievementID}_unlocked", 0) == 1;
#endif
    }
}