
using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class ObjectAchievementTrigger : MonoBehaviour
{
    [SerializeField] private string achievementID = "Lvl_1";
    [SerializeField] private bool destroyOnTouch = false; // Optional: remove object after trigger

    private bool unlocked = false;
    private string LocalKey => $"achieve_{achievementID}_unlocked";

    private void Start()
    {
#if !DISABLESTEAMWORKS
        if (!SteamManager.Initialized)
        {
            // Steam not initialized at runtime: fallback to local saved state
            unlocked = PlayerPrefs.GetInt(LocalKey, 0) == 1;
            Debug.Log($"[ObjectAchievementTrigger] Steam not initialized, loaded local unlocked={unlocked} for {achievementID}");
        }
        else
        {
            // Query Steam for the achievement state to avoid duplicate unlocks
            SteamUserStats.GetAchievement(achievementID, out bool alreadyUnlocked);
            unlocked = alreadyUnlocked;
        }
#else
        // Steam disabled at compile time: load local saved state
        unlocked = PlayerPrefs.GetInt(LocalKey, 0) == 1;
        Debug.Log($"[DISABLESTEAMWORKS] Loaded local unlocked={unlocked} for {achievementID}");
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.CompareTag("Player"))
        {
#if !DISABLESTEAMWORKS
            if (!SteamManager.Initialized)
            {
                // Steam not running at runtime -> local fallback
                unlocked = true;
                PlayerPrefs.SetInt(LocalKey, 1);
                PlayerPrefs.Save();
                Debug.Log($"[ObjectAchievementTrigger] Steam not initialized; locally unlocked {achievementID}");
            }
            else
            {
                SteamAchievementManager.UnlockAchievement(achievementID);
                unlocked = true;
            }
#else
            // Steam disabled at compile time -> local fallback
            unlocked = true;
            PlayerPrefs.SetInt(LocalKey, 1);
            PlayerPrefs.Save();
            Debug.Log($"[DISABLESTEAMWORKS] Locally unlocked achievement {achievementID}");
#endif

            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }
}