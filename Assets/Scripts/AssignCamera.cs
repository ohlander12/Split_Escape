using UnityEngine;
using Unity.Netcode;
using Unity.Cinemachine;

public class AssignCamera : NetworkBehaviour
{
    private CinemachineCamera vcam;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return; // Kun den lokale spiller skal styre sit kamera

        vcam = FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.Follow = transform;
            vcam.LookAt = transform;
        }
        else
        {
            Debug.LogWarning("CinemachineVirtualCamera not found in scene!");
        }
    }
}
