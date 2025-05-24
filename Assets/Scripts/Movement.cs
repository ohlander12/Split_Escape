using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 inputMovement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private NetworkVariable<bool> netFlipX = new NetworkVariable<bool>(
        false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public AudioClip attackSound;
    private AudioSource audioSource;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        netFlipX.OnValueChanged += (oldVal, newVal) =>
        {
            if (spriteRenderer != null)
                spriteRenderer.flipX = newVal;
        };

        if (!IsOwner && spriteRenderer != null)
        {
            spriteRenderer.flipX = netFlipX.Value;
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        var keyboard = Keyboard.current;
        inputMovement = Vector2.zero;

        if (keyboard.wKey.isPressed) inputMovement.y += 1;
        if (keyboard.sKey.isPressed) inputMovement.y -= 1;
        if (keyboard.aKey.isPressed) inputMovement.x -= 1;
        if (keyboard.dKey.isPressed) inputMovement.x += 1;

        inputMovement = inputMovement.normalized;

        if (animator != null)
        {
            animator.SetBool("run", inputMovement != Vector2.zero);

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                animator.SetTrigger("attack");

                if (audioSource != null && attackSound != null)
                {
                    audioSource.PlayOneShot(attackSound);
                }
            }
        }

        if (spriteRenderer != null && inputMovement.x != 0)
        {
            bool flip = inputMovement.x < 0;
            spriteRenderer.flipX = flip;
            netFlipX.Value = flip;
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        rb.MovePosition(rb.position + inputMovement * moveSpeed * Time.fixedDeltaTime);
    }
}
