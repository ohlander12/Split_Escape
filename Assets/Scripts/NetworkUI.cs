using UnityEngine;
using Unity.Netcode;

public class NetworkUI : MonoBehaviour
{
    private void OnGUI()
    {
        int buttonWidth = 150;
        int buttonHeight = 40;

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUI.Button(new Rect(10, 10, buttonWidth, buttonHeight), "Start Host"))
            {
                NetworkManager.Singleton.StartHost();
            }

            if (GUI.Button(new Rect(10, 60, buttonWidth, buttonHeight), "Start Client"))
            {
                NetworkManager.Singleton.StartClient();
            }

            if (GUI.Button(new Rect(10, 110, buttonWidth, buttonHeight), "Start Server"))
            {
                NetworkManager.Singleton.StartServer();
            }
        }
    }
}
