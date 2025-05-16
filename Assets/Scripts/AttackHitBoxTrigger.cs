using Unity.Netcode;
using UnityEngine;

public class AttackHitboxTrigger : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsOwner) return;

        if (other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            enemy.TakeDamageServerRpc(1); // du kan justere skaden her
        }

        if (other.TryGetComponent<LeverActivator>(out var lever))
        {
            lever.ActivateLeverServerRpc();
        }
    }
}
