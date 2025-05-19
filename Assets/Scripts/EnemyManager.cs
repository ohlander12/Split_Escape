using Unity.Netcode;
using UnityEngine;

public class EnemyManager : NetworkBehaviour
{
    public static EnemyManager Instance;
    public GameObject[] doorsToOpen;
    public int totalEnemies = 2;
    private int deadEnemies = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void NotifyEnemyDied()
    {
        deadEnemies++;
        if (deadEnemies >= totalEnemies)
        {
            OpenDoorsClientRpc();
        }
    }

    [ClientRpc]
    private void OpenDoorsClientRpc()
    {
        foreach (var door in doorsToOpen)
        {
            if (door != null)
                door.SetActive(false);
        }
    }
}
