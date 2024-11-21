using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Interactable Interactable;
    private Outline outline;

    public string objectName => Interactable.objectName;
    public string description => Interactable.description;
    public Sprite icon => Interactable.icon;
    

    private void Start()
    {
        //Interactable.associatedItem = this;

        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 10f;
        }
        outline.enabled = false;

        if (Interactable == null)
        {
            Debug.LogError($"{gameObject.name} has no Interactable assigned at runtime.");
        }
        else
        {
            Debug.Log($"Interactable for {gameObject.name} is assigned: {Interactable.objectName}");
        }

        Interactable.associatedItem = this;
    }

    public void ToggleOutline(bool toggle)
    {
        outline.enabled = toggle;
    }
    public void Interact()
    {
        Debug.Log("Item interaction triggered.");
        if (Interactable != null)
        {
            Debug.Log($"Interacting with: {Interactable.objectName}");
            Interactable.Interact();

            if(Interactable.interactionType == Interactable.InteractionType.PickupablItem)
            {
                if(Interactable.associatedItem != null)
                {
                    Debug.Log("Adding item to inventory.");
                    Inventory.Instance.AddItem(Interactable.associatedItem);

                }
                else
                {
                    Debug.LogError("Associated item is null!");
                }
            }
        }
        else
        {
            Debug.LogError($"[Item] Interactable is null for item: {gameObject.name}");
            return;  // Prevent further processing if Interactable is null
        }
    }

}
