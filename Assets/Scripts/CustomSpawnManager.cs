using UnityEngine;
using Unity.Netcode;

public class CustomSpawnManager : NetworkBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Spawner hostens egen spiller
            SpawnPlayer(NetworkManager.Singleton.LocalClientId);

            // Spawner nye klienter når de forbinder
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        // Undgå at spawne host igen
        if (clientId == NetworkManager.Singleton.LocalClientId) return;

        SpawnPlayer(clientId);
    }

    private void SpawnPlayer(ulong clientId)
    {
        GameObject prefabToSpawn;

        if (clientId == 0) // Host har altid clientId = 0
        {
            prefabToSpawn = player1Prefab;
        }
        else
        {
            prefabToSpawn = player2Prefab;
        }

        Vector3 spawnPosition = Vector3.right * (int)clientId * 2;
        var instance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        instance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }
}
