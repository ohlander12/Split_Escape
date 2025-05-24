using UnityEngine;
using Unity.Netcode;
using Unity.Cinemachine;

public class AssignCamera : NetworkBehaviour
{
    private CinemachineCamera vcam;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        vcam = FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.Follow = transform;
            vcam.LookAt = transform;
        }
    }
}
