using Unity.Services.Relay;
using Unity.Services.Core;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerManager : NetworkBehaviour
{
    private string joinCode = "";  // Variabel til at gemme join-koden

    private void Start()
    {
        if (IsHost)
        {
            // Initialiser Relay-tjenesten, hvis vi er vært
            InitializeRelay();
        }
        else
        {
            // Hvis ikke, skal klienten vente på serverens join-kode
            Debug.Log("Waiting to connect to the server...");
        }
    }

    private async void InitializeRelay()
    {
        // Initialiser Unity Services
        await UnityServices.InitializeAsync();

        // Opret en Relay-allokering for værtsenheden (1 spiller til at starte med)
        var allocation = await RelayService.Instance.CreateAllocationAsync(1); // 1 spiller (du kan ændre dette antal, hvis nødvendigt)
        joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        // Start Relay-serveren (vært)
        NetworkManager.Singleton.StartHost();
        Debug.Log("Host is ready. Join code: " + joinCode);
    }

    public void JoinGame(string code)
    {
        // Hvis vi er en klient, skal vi prøve at slutte os til serveren ved hjælp af join-koden
        if (IsClient)
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("Joining game using join code: " + code);
        }
    }

    // For eksempel: synkronisering af spillerens position
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            // Initialiser spillerens kontrol (kun på lokal spiller)
            Debug.Log("Local player connected");
        }
    }

    // Synkroniser position for lokal spiller
    private void Update()
    {
        if (IsOwner)
        {
            // Opdater position for lokal spiller og send data til netværket
            transform.position = new Vector3(0, 0, 0); // Eksempel: sæt position til (0,0,0)
        }
    }
}
