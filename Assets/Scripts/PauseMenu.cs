using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject GameOverMenu;

    public static bool isPaused;
    public static bool isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        isPaused = false; // Reset the pause state when the scene starts
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver) 
        {
            if (isPaused)
            {
                Debug.Log("ResumeGame called with Escape");
                ResumeGame();
            }
            else
            {
                Debug.Log("PauseGame called with Escape");
                PauseGame();
            }
        }
        
    }

    public void PauseGame() 
    {
        Debug.Log("PauseGame called");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        isGameOver = true;

        // Show and unlock the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame called");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowGameOver() 
    {
        Debug.Log("GameOver called");
        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        isGameOver = true;
        AudioManager.Instance.Stop("StartingAmbaince");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void GoToMainMenu() 
    {
        Debug.Log("MainMenu called");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        AudioManager.Instance.Stop("StartingAmbaince");

        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
