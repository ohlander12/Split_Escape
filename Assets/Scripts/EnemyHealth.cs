using Unity.Netcode;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    public int maxHealth = 3;
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    private void Start()
    {
        currentHealth.Value = maxHealth;
    }

    [ServerRpc]
    public void TakeDamageServerRpc(int damage)
    {
        if (!IsServer) return;

        currentHealth.Value -= damage;

        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Her kan du spille en animation, droppe loot eller andet
        Destroy(gameObject); // fjern fjenden for alle
    }
}
