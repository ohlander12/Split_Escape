using Unity.Netcode;
using UnityEngine;

public class LeverActivator : NetworkBehaviour
{
    private bool isActivated = false;
    public GameObject doorToOpen;

    [ServerRpc(RequireOwnership = false)]
    public void ActivateLeverServerRpc()
    {
        if (isActivated) return;

        isActivated = true;
        ActivateLeverClientRpc();

        if (doorToOpen != null)
        {
            // Fjern døren på alle klienter via ClientRpc
            DeactivateDoorClientRpc();
        }
    }

    [ClientRpc]
    private void ActivateLeverClientRpc()
    {
        Debug.Log("Lever activated!");
        // Du kan spille lyd/animation her også
    }

    [ClientRpc]
    private void DeactivateDoorClientRpc()
    {
        if (doorToOpen != null)
        {
            doorToOpen.SetActive(false);
        }
    }
}
