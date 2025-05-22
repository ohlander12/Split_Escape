using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreedomZone : NetworkBehaviour
{
    private int playersInside = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return; // Kun server må håndtere dette

        if (other.CompareTag("Player"))
        {
            playersInside++;

            if (playersInside >= 2)
            {
                Debug.Log("Begge spillere er inde i frihedszonen!");

                // Despawn begge spillere sikkert
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in players)
                {
                    NetworkObject netObj = player.GetComponent<NetworkObject>();
                    if (netObj != null && netObj.IsSpawned)
                    {
                        netObj.Despawn();
                    }
                }

                // Load "You Won"-scenen for alle
                LoadWinSceneClientRpc();
            }
        }
    }

    [ClientRpc]
    private void LoadWinSceneClientRpc()
    {
        SceneManager.LoadScene("WinScene"); // Skift navnet hvis din scene hedder noget andet
    }
}
