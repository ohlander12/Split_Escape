using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreedomZone : NetworkBehaviour
{
    private int playersInside = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;

        if (other.CompareTag("Player"))
        {
            playersInside++;

            if (playersInside >= 2)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in players)
                {
                    NetworkObject netObj = player.GetComponent<NetworkObject>();
                    if (netObj != null && netObj.IsSpawned)
                    {
                        netObj.Despawn();
                    }
                }

                LoadWinSceneClientRpc();
            }
        }
    }

    [ClientRpc]
    private void LoadWinSceneClientRpc()
    {
        SceneManager.LoadScene("WinScene");
    }
}
