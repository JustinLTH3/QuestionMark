using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Toggle inputMode;
    [SerializeField] TMP_Text inputModeTxt;
    UnityAction<bool> ChangeInputMode;
    private void start()
    {
        inputModeTxt.text = Settings.instance.GetStringInputMode();
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
            inputModeTxt.text = Settings.instance.GetStringInputMode();
        };

        inputMode.onValueChanged.AddListener(ChangeInputMode);
    }
}
