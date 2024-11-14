using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [Header("Game References")]
    public PlayerScript player;
    public DemonScript demon;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setIsPlayerLooking(bool b)
    {
        demon.isPlayerLooking = b;
    }
}
