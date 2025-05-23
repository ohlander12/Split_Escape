using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Health : NetworkBehaviour
{
    public int maxHealth = 10;
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    private Animator animator;
    private Rigidbody2D rb;
    private Movement movementScript;
    private HealthUI healthUI;

    private bool isDead = false;

    private void Start()
    {
        if (IsServer)
        {
            currentHealth.Value = maxHealth;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();

        if (IsOwner)
        {
            healthUI = FindAnyObjectByType<HealthUI>();
            if (healthUI != null)
            {
                healthUI.UpdateHearts(currentHealth.Value);
            }

            currentHealth.OnValueChanged += OnHealthChanged;
        }
    }

    private void OnHealthChanged(int oldValue, int newValue)
    {
        if (IsOwner && healthUI != null)
        {
            healthUI.UpdateHearts(newValue);
        }
    }

    public void TakeDamage(int amount)
    {
        if (IsOwner)
        {
            TakeDamageServerRpc(amount);
        }
    }

    // Bruges af EnemyAI direkte – virker både på klient og host
    public void TakeDamagePublic(int amount)
    {
        TakeDamageServerRpc(amount);
    }

    [ServerRpc(RequireOwnership = false)]
    private void TakeDamageServerRpc(int amount)
    {
        if (isDead) return;

        currentHealth.Value -= amount;

        if (currentHealth.Value <= 0)
        {
            currentHealth.Value = 0;
            DieClientRpc();

            if (IsServer)
            {
                // Start coroutine med delay før sceneskift
                StartCoroutine(LoadLoseSceneAfterDelay(2f));
            }
        }
    }

    private IEnumerator LoadLoseSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NetworkManager.SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
    }

    [ClientRpc]
    private void DieClientRpc()
    {
        if (isDead) return;
        isDead = true;

        animator.SetTrigger("die");
        rb.linearVelocity = Vector2.zero;

        if (movementScript != null)
            movementScript.enabled = false;

        // GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (IsOwner && Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(999);
        }
    }
}
