using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyAI : NetworkBehaviour
{
    public float moveSpeed = 3f;
    public int damageAmount = 10;
    public float attackCooldown = 1f;
    public float attackRange = 0.5f;
    public float aggroRange = 6f;

    private Rigidbody2D rb;
    private Animator animator;
    private float lastAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) return;

        GameObject targetPlayer = GetClosestPlayerInRange(players, aggroRange);
        if (targetPlayer == null)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float dist = Vector2.Distance(transform.position, targetPlayer.transform.position);

        if (dist > attackRange)
        {
            Vector2 dir = (targetPlayer.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        CheckForPlayerCollisionDamage();
    }

    private GameObject GetClosestPlayerInRange(GameObject[] players, float range)
    {
        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (var player in players)
        {
            if (player == null) continue;

            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (dist < minDist && dist <= range)
            {
                minDist = dist;
                closest = player;
            }
        }

        return closest;
    }

    private void CheckForPlayerCollisionDamage()
    {
        if (!IsServer) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player") && Time.time - lastAttackTime >= attackCooldown)
            {
                Health health = hit.GetComponent<Health>();
                if (health != null)
                {
                    // Brug den nye public metode der sikrer korrekt RPC
                    health.TakeDamagePublic(damageAmount);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
