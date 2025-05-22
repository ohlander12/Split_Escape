using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class WinSceneUI : MonoBehaviour
{
    public void PlayAgain()
    {
        // Luk netværket først
        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.Shutdown();
        }

        // Fjern NetworkManager manuelt hvis den er sat til DontDestroyOnLoad
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }

        // Gå tilbage til MainMenu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
