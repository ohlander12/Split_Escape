using Unity.Netcode;
using UnityEngine;

public class PuzzleSequenceManager : NetworkBehaviour
{
    [SerializeField] private GameObject[] doorsToOpen;
    private string[] correctOrder = { "A", "B", "C", "D" };
    private NetworkVariable<int> currentStep = new NetworkVariable<int>(0);

    public void StepOnPlatform(string id)
    {
        StepOnPlatformServerRpc(id);
    }

    [ServerRpc(RequireOwnership = false)]
    private void StepOnPlatformServerRpc(string id)
    {
        if (id == correctOrder[currentStep.Value])
        {
            currentStep.Value++;

            if (currentStep.Value == correctOrder.Length)
            {
                OpenDoorsClientRpc();
            }
        }
        else
        {
            currentStep.Value = 0; 
        }
    }

    [ClientRpc]
    private void OpenDoorsClientRpc()
    {
        foreach (var door in doorsToOpen)
        {
            door.SetActive(false);
        }
    }
}
