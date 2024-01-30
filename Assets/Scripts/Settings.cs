using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    public InputMode InputMode_ { get; private set; }

    public float MusicVolume { get; private set; }

    public UnityEvent OnInputModeChange;
    public UnityEvent OnVolumeChange;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);
        InputMode_ = (InputMode)PlayerPrefs.GetInt("InputMode", (int)InputMode.Swipe);
    }

    public string GetStringInputMode()
    {
        switch (InputMode_)
        {
            case InputMode.Swipe: return "Swipe";
            case InputMode.Keyboard: return "Keyboard";
            case InputMode.Touch: return "Touch";
        }

        return "";
    }

    public void ChangeInputMode(InputMode value)
    {
        if (value == InputMode_) return;
        InputMode_ = value;
        PlayerPrefs.SetInt("InputMode", (int)value);
        PlayerPrefs.Save();
        OnInputModeChange.Invoke();
    }

    public void ChangeMusicVolume(float value)
    {
        if (value == MusicVolume) return;
        MusicVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
        OnVolumeChange.Invoke();
    }
}