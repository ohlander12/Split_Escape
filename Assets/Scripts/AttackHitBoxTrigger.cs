using Unity.Netcode;
using UnityEngine;

public class AttackHitboxTrigger : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsOwner) return;

        if (other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            enemy.TakeDamageServerRpc(1);
        }

        if (other.TryGetComponent<BossHealth>(out var boss))
        {
            boss.TakeDamageServerRpc(1);
        }

        if (other.TryGetComponent<LeverActivator>(out var lever))
        {
            lever.ActivateLeverServerRpc();
        }

        if (other.TryGetComponent<PlatformTrigger>(out var platform))
        {
            platform.ActivatePlatformServerRpc();
        }
    }
}
