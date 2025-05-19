using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class MainMenuUI : MonoBehaviour
{
    public InputField ipInputField;
    public Button hostButton;
    public Button clientButton;

    public void StartAsHost()
    {
        if (NetworkManager.Singleton.IsListening) return;

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = "0.0.0.0"; // Lyt på alle adresser som host

        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

        hostButton.interactable = false;
    }

    public void StartAsClient()
    {
        if (NetworkManager.Singleton.IsListening) return;

        var ip = ipInputField.text;
        if (string.IsNullOrEmpty(ip)) ip = "127.0.0.1"; // fallback

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = ip;

        NetworkManager.Singleton.StartClient();

        clientButton.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
