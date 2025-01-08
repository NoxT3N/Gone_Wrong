using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance {  get; private set; }

    [Header("Inventory Settings")]
    public int maxItems = 3;
    public List<GameObject> slots;

    private List<Item> items = new List<Item>();
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"Added {item.name} to inventory.");

           foreach(GameObject slot in slots)
            {
                if(slot.transform.childCount == 0)
                {
                    GameObject newItem = new GameObject(item.objectName);
                    Image itemIcon = newItem.AddComponent<Image>();
                    itemIcon.sprite = item.icon;
                    itemIcon.preserveAspect = true;

                    itemIcon.transform.SetParent(slot.transform, false);
                    itemIcon.transform.localPosition = Vector3.zero;
                    itemIcon.transform.localScale = new Vector3(8, 8, 8);

                    Debug.Log($"Assigned sprite to slot");

                    item.gameObject.SetActive(false);

                    return;
                }
            }
        
        
            
    }

    public void RemoveItem(Item item)
    {
       items.Remove(item);

        foreach(GameObject slot in slots)
        {
            if(slot.transform.childCount > 0)
            {
                Transform child = slot.transform.GetChild(0);
                if(child.name == item.objectName)
                {
                    Destroy(child.gameObject);
                    Debug.Log($"Removed {item.objectName} from inventory.");
                    return; 
                }
            }
        }
    }

 

}
