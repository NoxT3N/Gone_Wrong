using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance {  get; private set; }

    [Header("Invetory UI")]
    [SerializeField] private Image[] inventorySlots;

    public void AddItemToSlot(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            
        }
    }

    public void RemoveItemFromSlot(Item item)
    {
        for(int i = 0;i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].sprite == item.icon)
            {
                
            }
        }
    }
   
}
