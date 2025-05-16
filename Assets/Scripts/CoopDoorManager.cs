using Unity.Netcode;
using UnityEngine;

public class CoopDoorManager : NetworkBehaviour
{
    public GameObject[] doorsToOpen;

    private int playersOnPlate = 0;

    [ServerRpc(RequireOwnership = false)]
    public void PlayerSteppedOnPlateServerRpc()
    {
        playersOnPlate++;

        if (playersOnPlate >= 2)
        {
            DeactivateDoorsClientRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void PlayerSteppedOffPlateServerRpc()
    {
        playersOnPlate--;

        if (playersOnPlate < 0) playersOnPlate = 0;
    }

    [ClientRpc]
    private void DeactivateDoorsClientRpc()
    {
        foreach (GameObject door in doorsToOpen)
        {
            if (door != null)
                door.SetActive(false);
        }
    }
}
