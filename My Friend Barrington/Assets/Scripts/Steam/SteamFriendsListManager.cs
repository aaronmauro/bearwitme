using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif
using System.Collections.Generic;

public class SteamFriendsListManager : MonoBehaviour
{
#if !DISABLESTEAMWORKS
    private List<CSteamID> friendsList = new List<CSteamID>();
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
#endif
    }

    public void GetFriendsList()
    {
#if !DISABLESTEAMWORKS
        friendsList.Clear();

        int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
        Debug.Log("Found " + friendCount + " friends.");

        for (int i = 0; i < friendCount; i++)
        {
            CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            friendsList.Add(friendSteamID);

            string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);
            EPersonaState friendState = SteamFriends.GetFriendPersonaState(friendSteamID);
            Debug.Log($"Friend {i}: {friendName} ({friendSteamID.m_SteamID}), Status: {friendState}");
        }

        UpdateFriendsUI();
#endif
    }

    private void UpdateFriendsUI()
    {
        // Add code here to populate your Unity UI with the friendsList data.
        // For example, displaying their names and whether they are online.
    }

    public void OpenInviteOverlay(
#if !DISABLESTEAMWORKS
        CSteamID lobbyID
#else
        ulong lobbyID // fallback type when Steamworks is disabled
#endif
    )
    {
#if !DISABLESTEAMWORKS
        // Note: This often does not work in the Unity editor and needs a build to test properly
        SteamFriends.ActivateGameOverlayInviteDialog(lobbyID);
#endif
    }
}