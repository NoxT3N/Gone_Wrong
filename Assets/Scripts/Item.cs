using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Interactable Interactable;
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 10f;
        }
        outline.enabled = false;
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
