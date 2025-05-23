using TMPro;
using UnityEngine;

public class JoinCodeDisplay : MonoBehaviour
{
    public TMP_Text joinCodeText;

    void Start()
    {
        // S�t teksten til join koden gemt i MainMenuUI.CurrentJoinCode
        if (joinCodeText != null)
        {
            joinCodeText.text = "Join Code: " + MainMenuUI.CurrentJoinCode;
        }
    }


}
