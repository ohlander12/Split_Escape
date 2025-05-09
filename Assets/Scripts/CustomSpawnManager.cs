using UnityEngine;
using Unity.Netcode;

public class CustomSpawnManager : NetworkBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform player1Spawn;
    public Transform player2Spawn;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SpawnPlayer(NetworkManager.Singleton.LocalClientId);
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId) return;
        SpawnPlayer(clientId);
    }

    private void SpawnPlayer(ulong clientId)
    {
        GameObject prefabToSpawn;
        Vector3 spawnPosition;

        if (clientId == 0)
        {
            prefabToSpawn = player1Prefab;
            spawnPosition = player1Spawn.position;
        }
        else
        {
            prefabToSpawn = player2Prefab;
            spawnPosition = player2Spawn.position;
        }

        var instance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        instance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }
}
