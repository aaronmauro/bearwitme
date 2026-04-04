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
#endif
    }
}