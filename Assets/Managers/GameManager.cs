using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    public PlayerScript player;
    public DemonScript demon;

    [Header("UI References")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 5f;

    private Item currentItem;

    public bool IsPaused { get; private set; } = false;
    public bool IsGameOver { get; private set; } = false;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsGameOver)
        {
            if (IsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        IsGameOver = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetIsPlayerLookingAtDemon(bool b)
    {
        if (demon != null)
        {
            demon.isPlayerLooking = b;
        }
        else
        {
            Debug.LogError("Demon is null.");
        }
    }

    public bool CanInteract(Item item)
    {
        return item != null && Vector3.Distance(player.transform.position, item.transform.position) <= interactionDistance;
    }

    public void OutlineItem(Item item)
    {
        if (currentItem != null)
        {
            currentItem.ToggleOutline(false);
        }
        currentItem = item;
        currentItem.ToggleOutline(true);
    }

    public void ClearOutline()
    {
        if (currentItem != null)
        {
            currentItem.ToggleOutline(false);
            currentItem = null;
        }
    }
}
