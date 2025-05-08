using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Health : NetworkBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private Rigidbody2D rb;
    private Movement movementScript;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Spil death-animation
        animator.SetTrigger("die");

        // Stop al bevægelse
        rb.linearVelocity = Vector2.zero;

        // Slå movement fra
        if (movementScript != null)
            movementScript.enabled = false;

        // (Valgfrit) Deaktiver collider:
        // GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(999); // Eller SetHealth(0), afhængigt af din metode
        }
    }

}
