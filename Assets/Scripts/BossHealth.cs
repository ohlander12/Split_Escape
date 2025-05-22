using Unity.Netcode;
using UnityEngine;

public class BossHealth : NetworkBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRpc(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            BossManager.Instance.NotifyEnemyDied();
            Destroy(gameObject);
        }
    }
}