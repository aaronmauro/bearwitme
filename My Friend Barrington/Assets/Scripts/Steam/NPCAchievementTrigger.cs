using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class NPCAchievementTrigger : MonoBehaviour
{
    [SerializeField] private string achievementID = "NPCM";
    private bool unlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.CompareTag("Player"))
        {
#if !DISABLESTEAMWORKS
            SteamAchievementManager.UnlockAchievement(achievementID);
#endif
            unlocked = true;
        }
    }
}