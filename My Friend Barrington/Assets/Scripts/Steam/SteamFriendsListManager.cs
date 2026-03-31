using UnityEngine;

using System.Collections.Generic;

public class SteamFriendsListManager : MonoBehaviour
{
#if !DISABLESTEAMWORKS
    // A list to store the CSteamIDs of the user's friends (Steam builds)
    private List<CSteamID> friendsList = new List<CSteamID>();
#else
    // Local fallback: store friend identifiers or names as strings (non‑Steam builds)
    private List<string> friendsList = new List<string>();
    private const string LocalFriendsKey = "local_friends"; // stored as 'name1|name2|name3'
#endif

    void Start()
    {
#if !DISABLESTEAMWORKS
        if (SteamManager.Initialized)
        {
            Debug.Log("Steam Manager initialized. Getting friends list...");
            GetFriendsList();
        }
        else
        {
            Debug.LogError("Steam Manager not initialized. Make sure Steamworks.NET is set up correctly and Steam is running.");
        }
#else
        // Load locally saved friend list (if any) so non‑Steam builds can show something
        friendsList.Clear();
        string saved = PlayerPrefs.GetString(LocalFriendsKey, "");
        if (!string.IsNullOrEmpty(saved))
        {
            var parts = saved.Split('|');
            foreach (var p in parts)
            {
                if (!string.IsNullOrEmpty(p))
                    friendsList.Add(p);
            }
        }
        Debug.Log($"[DISABLESTEAMWORKS] Loaded {friendsList.Count} local friends.");
        UpdateFriendsUI();
#endif
    }

#if !DISABLESTEAMWORKS
    public void GetFriendsList()
    {
        friendsList.Clear();

        // Get the number of friends the user has
        int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
        Debug.Log("Found " + friendCount + " friends.");

        for (int i = 0; i < friendCount; i++)
        {
            // Get the Steam ID of each friend
            CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            friendsList.Add(friendSteamID);

            // You can also retrieve more information, such as their name and presence
            string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);
            EPersonaState friendState = SteamFriends.GetFriendPersonaState(friendSteamID);

            // Use ToString() to avoid accessing internal fields directly
            Debug.Log($"Friend {i}: {friendName} ({friendSteamID.ToString()}), Status: {friendState}");
        }

        // Example of what to do next: maybe update a UI list of friends
        UpdateFriendsUI();
    }
#else
    // Local variant for non‑Steam builds
    public void GetFriendsList()
    {
        // friendsList is already loaded in Start(); here we just re-populate UI.
        Debug.Log($"[DISABLESTEAMWORKS] GetFriendsList called — {friendsList.Count} local friends available.");
        UpdateFriendsUI();
    }

    // Helper to add a local friend (for testing / editor usage)
    public void AddLocalFriend(string friendName)
    {
        if (string.IsNullOrEmpty(friendName)) return;
        if (friendsList.Contains(friendName)) return;
        friendsList.Add(friendName);
        SaveLocalFriends();
    }

    private void SaveLocalFriends()
    {
        PlayerPrefs.SetString(LocalFriendsKey, string.Join("|", friendsList));
        PlayerPrefs.Save();
    }
#endif

    private void UpdateFriendsUI()
    {
        // Add code here to populate your Unity UI with the friendsList data.
        // For Steam builds friendsList contains CSteamID entries; for non‑Steam builds it contains strings.
        // Example: iterate and display names/IDs accordingly.
    }

#if !DISABLESTEAMWORKS
    // Example of how to use the Steam Overlay to invite friends to a lobby (Steam builds)
    public void OpenInviteOverlay(CSteamID lobbyID)
    {
        // Note: This often does not work in the Unity editor and needs a build to test properly
        SteamFriends.ActivateGameOverlayInviteDialog(lobbyID);
    }
#else
    // Non‑Steam builds: provide a safe overload that simply logs the request
    public void OpenInviteOverlay(string lobbyID)
    {
        Debug.Log($"[DISABLESTEAMWORKS] OpenInviteOverlay called with lobbyID: {lobbyID}. Steam disabled, cannot open overlay.");
    }
#endif
}