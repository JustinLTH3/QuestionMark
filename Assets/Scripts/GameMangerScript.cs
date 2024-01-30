using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    bool vibrate;
    [SerializeField] Slider volume;
    public bool GetVibrate
    {
        get
        {

            if (vibrate)
            {
                Handheld.Vibrate();
            }

            return vibrate;
        }

    }

    public void AudioValueChange(Slider slider)
    {
        PlayerPrefs.SetFloat("AudioLevel", slider.value);
        PlayerPrefs.Save();
        AudioListener.volume = slider.value;
    }

    void start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("AudioLevel", 1);
        FindObjectOfType<Slider>().value = PlayerPrefs.GetFloat("AudioLevel", 1);

        vibrate = (PlayerPrefs.GetInt("Vibrate", 1) == 1);

        FindAnyObjectByType<Toggle>().isOn = vibrate;
        volume.value = AudioListener.volume;
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
