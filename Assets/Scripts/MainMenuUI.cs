using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartAsHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void StartAsClient()
    {
        NetworkManager.Singleton.StartClient();
        // Scene skift sker automatisk når host loader den
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}