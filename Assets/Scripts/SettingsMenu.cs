using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Toggle inputMode;
    UnityAction<bool> ChangeInputMode;
    private void Start()
    {
        inputMode.isOn = Settings.instance.InputMode_ == InputMode.Swipe ? true : false;
        ChangeInputMode = (bool value) =>
        {
            if (value)
            {
                Settings.instance.ChangeInputMode(InputMode.Swipe);
            }
            else
            {
                Settings.instance.ChangeInputMode(InputMode.Touch);
            }
        };

        inputMode.onValueChanged.AddListener(ChangeInputMode);
    }
    public void SetHighScoreText(TMP_Text txt)
    {
        txt.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
