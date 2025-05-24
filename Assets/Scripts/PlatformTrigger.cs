using Unity.Netcode;
using UnityEngine;

public class PlatformTrigger : NetworkBehaviour
{
    [SerializeField] private PuzzleSequenceManager manager;
    [SerializeField] private string platformID;

    [ServerRpc(RequireOwnership = false)]
    public void ActivatePlatformServerRpc()
    {
        manager.StepOnPlatform(platformID);
    }
}
