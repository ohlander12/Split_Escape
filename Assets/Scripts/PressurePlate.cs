using Unity.Netcode;
using UnityEngine;

public class PressurePlate : NetworkBehaviour
{
    public CoopDoorManager doorManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;

        if (other.CompareTag("Player"))
        {
            doorManager.PlayerSteppedOnPlateServerRpc();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsServer) return;

        if (other.CompareTag("Player"))
        {
            doorManager.PlayerSteppedOffPlateServerRpc();
        }
    }
}
