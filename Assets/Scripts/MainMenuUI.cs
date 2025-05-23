using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Authentication;
using TMPro;  // Husk denne using for TMP
using System.Threading.Tasks;
using Unity.Networking.Transport.Relay;

public class MainMenuUI : MonoBehaviour
{
    private bool unityServicesInitialized = false;

    [Header("UI References")]
    public TMP_InputField JoinCodeInputField;

    // Gem join koden her, s� andre scripts kan tilg� den
    public static string CurrentJoinCode = "";

    public async void StartAsHost()
    {
        await InitializeUnityServices();

        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Relay Join Code: " + joinCode);

            // Gem join koden i static variablen
            CurrentJoinCode = joinCode;

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(new RelayServerData(allocation, "dtls"));

            NetworkManager.Singleton.StartHost();

            // Her kan du evt. s�tte en delayed scene load, hvis du vil v�re sikker p� hosten er startet f�rst
            NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Relay Host Error: " + e.Message);
        }
    }

    public async void StartAsClient()
    {
        string joinCode = JoinCodeInputField.text.Trim();

        if (string.IsNullOrEmpty(joinCode))
        {
            Debug.LogError("Join code is empty!");
            return;
        }

        await InitializeUnityServices();

        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Relay Client Error: " + e.Message);
        }
    }

    private async Task InitializeUnityServices()
    {
        if (!unityServicesInitialized)
        {
            await UnityServices.InitializeAsync();
            unityServicesInitialized = true;

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
