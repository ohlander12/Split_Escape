using TMPro;
using UnityEngine;

public class JoinCodeDisplay : MonoBehaviour
{
    public TMP_Text joinCodeText;

    void Start()
    {
        if (joinCodeText != null)
        {
            joinCodeText.text = "Join Code: " + MainMenuUI.CurrentJoinCode;
        }
    }

}
