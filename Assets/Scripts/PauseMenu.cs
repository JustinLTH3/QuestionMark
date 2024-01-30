using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button Button;
    bool check = false;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void OnPauseButtonClick()
    { 
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
       
    }

    public void OnResumeButtonClick()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        //Button.gameObject.SetActive(true);
    }
}
