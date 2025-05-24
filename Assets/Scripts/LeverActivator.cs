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

        if (doorToOpen != null)
        {
            DeactivateDoorClientRpc();
        }
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
