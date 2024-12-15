using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        Time.timeScale = 1.0f;
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        if (pauseMenu != null) 
        {
            pauseMenu.SetActive(false);
        }

        SceneManager.LoadScene("IteractScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
