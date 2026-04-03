using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class ObjectAchievementTrigger : MonoBehaviour
{
    [SerializeField] private string achievementID = "Lvl_1";
    [SerializeField] private bool destroyOnTouch = false;
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

            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }
}