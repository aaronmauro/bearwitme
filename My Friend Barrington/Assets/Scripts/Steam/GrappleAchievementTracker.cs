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
        SteamUserStats.GetStat(StatKey, out grappleCount);
        Debug.Log($"Grapple count loaded: {grappleCount}");
#endif
    }

    public static void RegisterGrapple()
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized) return;

        SteamUserStats.GetAchievement(AchievementID, out bool alreadyUnlocked);
        if (alreadyUnlocked) return;

        grappleCount++;
        Debug.Log($"Grapple used! Count: {grappleCount}/{RequiredCount}");

        SteamUserStats.SetStat(StatKey, grappleCount);
        SteamUserStats.StoreStats();

        if (grappleCount >= RequiredCount)
        {
            SteamAchievementManager.UnlockAchievement(AchievementID);
        }
#endif
    }
}