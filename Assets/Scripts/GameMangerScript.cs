using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    bool vibrate;
    [SerializeField] Slider volume;
    [SerializeField] Toggle vibration;

    public void AudioValueChange(Slider slider)
    {
        PlayerPrefs.SetFloat("AudioLevel", slider.value);
        PlayerPrefs.Save();
        AudioListener.volume = slider.value;
    }

    void start()
    {
        volume.value = AudioListener.volume;
        AudioListener.volume = PlayerPrefs.GetFloat("AudioLevel", 1);

        vibrate = (PlayerPrefs.GetInt("Vibrate", 1) == 1);
        vibration.isOn = vibrate;
    }

    public void Vibrate()
    {
        if (vibrate)
            Handheld.Vibrate();
    }

    public void VibrationToggleChange(Toggle toggle)
    {
        PlayerPrefs.SetInt("Vibrate", toggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        vibrate = toggle.isOn;
    }
}
