using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    public PlayerScript player;
    public DemonScript demon;
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 5f;

    private Item currentItem;

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

    public void SetIsPlayerLookingAtDemon(bool b)
    {
        demon.isPlayerLooking = b;
    }

    public bool CanInteract(Item item)
    {
        return item != null && Vector3.Distance(player.transform.position, item.transform.position) <= interactionDistance;
    }

    public void OutlineItem(Item item)
    {
        if(currentItem != null)
        {
            currentItem.ToggleOutline(false);
        }
        currentItem = item;
        currentItem.ToggleOutline(true);
    }

    public void ClearOutline()
    {
        if(currentItem != null)
        {
            currentItem.ToggleOutline(false);
            currentItem = null;
        }
    }
}
