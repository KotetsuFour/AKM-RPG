using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public static class StaticNetworking
{
    public static bool waiting = false;

    public async static void initialize()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            StaticData.playerLoginId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"Signed In As {StaticData.playerLoginId}");
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async static void createRelay(TextMeshProUGUI display)
    {
        Allocation all = await RelayService.Instance.CreateAllocationAsync(1);
        display.text = await RelayService.Instance.GetJoinCodeAsync(all.AllocationId);

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
            all.RelayServer.IpV4,
            (ushort)all.RelayServer.Port,
            all.AllocationIdBytes,
            all.Key,
            all.ConnectionData
        );
        NetworkManager.Singleton.StartHost();
        StaticData.player = 0;
        waiting = true;
    }

    public static void cancelConnection()
    {
        waiting = false;
        NetworkManager.Singleton.Shutdown();
    }

    public async static void joinRelay(string joinCode)
    {
        JoinAllocation all = await RelayService.Instance.JoinAllocationAsync(joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
            all.RelayServer.IpV4,
            (ushort)all.RelayServer.Port,
            all.AllocationIdBytes,
            all.Key,
            all.ConnectionData,
            all.HostConnectionData
        );
        NetworkManager.Singleton.StartClient();
        StaticData.player = 1;
        StaticData.numPlayers = 2;
    }

    public static void switchScenesWhenReady()
    {
        if (waiting && NetworkManager.Singleton.ConnectedClientsList.Count == StaticData.numPlayers)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Boardgame",
                UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
