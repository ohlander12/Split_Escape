using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    [Header("Prefab og spawnpunkter")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public override void OnNetworkSpawn()
    {
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
            enemyInstance.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
