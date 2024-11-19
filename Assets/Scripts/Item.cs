using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Interactable Interactable;
    private Outline outline;

    private void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.enabled = false;
        }
        else
        {
            Debug.Log("Outline Component Null");
        }
        
    }

    public void ToggleOutline(bool toggle)
    {
        outline.enabled = toggle;
    }
    public void Interact()
    {
        if (Interactable != null)
        {
            Interactable.Interact();
        }
    }

}
