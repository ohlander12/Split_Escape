using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    [Header("Prefab og spawnpunkter")]
    public GameObject enemyPrefab; // Fjenden skal have NetworkObject og EnemyHealth
    public Transform[] spawnPoints; // Tildel empty GameObjects i editoren

    public override void OnNetworkSpawn()
    {
        // Kun host/server må spawne fjender
        if (IsServer)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyInstance.GetComponent<NetworkObject>().Spawn(true); // spawn over netværket
        }
    }
}
