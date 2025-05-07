using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 inputMovement;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        { 
            enabled = false;
            return; 
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        rb.MovePosition(rb.position + inputMovement * moveSpeed * Time.fixedDeltaTime);
    }
}
