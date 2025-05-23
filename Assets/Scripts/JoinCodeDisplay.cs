using TMPro;
using UnityEngine;

public class JoinCodeDisplay : MonoBehaviour
{
    public TMP_Text joinCodeText;

    void Start()
    {
        // Sæt teksten til join koden gemt i MainMenuUI.CurrentJoinCode
        if (joinCodeText != null)
        {
            joinCodeText.text = "Join Code: " + MainMenuUI.CurrentJoinCode;
        }
    }


}
