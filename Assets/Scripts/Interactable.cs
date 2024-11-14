using UnityEngine;

[CreateAssetMenu(fileName = "Interactable", menuName = "Scriptable Objects/Interactable")]
public class Interactable : ScriptableObject
{
    public string objectName;
    public string description;
    public InteractionType interactionType;
    
    public virtual void Interact()
    {
        Debug.Log($"{objectName} interacted with!");
    }

    public enum InteractionType
    {
        PickupablItem,
        EnvirontmentalItem,
        Door
    }
}
