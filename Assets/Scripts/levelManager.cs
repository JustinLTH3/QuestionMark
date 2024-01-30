using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class levelManager : MonoBehaviour
{

    public string sceneName = "Game";
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
