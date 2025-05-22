using Unity.Netcode;
using UnityEngine;

public class PlatformTrigger : NetworkBehaviour
{
    [SerializeField] private PuzzleSequenceManager manager;
    [SerializeField] private string platformID;

    [ServerRpc(RequireOwnership = false)]
    public void ActivatePlatformServerRpc()
    {
        Debug.Log($"Platform {platformID} triggered"); // besked i konsollen
        manager.StepOnPlatform(platformID);
    }
}
