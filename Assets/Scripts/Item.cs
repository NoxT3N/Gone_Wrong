using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Interactable Interactable;

    public void Interact()
    {
        if (Interactable != null)
        {
            Interactable.Interact();
        }
    }
}
