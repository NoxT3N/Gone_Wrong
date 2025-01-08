using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject inventorySlots;

    public bool isPaused { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        inventorySlots.SetActive(true);
        isPaused = false;
        isGameOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame called");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        inventorySlots.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame called");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        inventorySlots.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }



    public void ShowGameOver()
    {
        Debug.Log("GameOver called");
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isGameOver = true;

        inventorySlots.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance?.Stop("StartingAmbaince");
    }

    public void GoToMainMenu()
    {
        Debug.Log("MainMenu called");
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;

        AudioManager.Instance?.Stop("StartingAmbaince");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
