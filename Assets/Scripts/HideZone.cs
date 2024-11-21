using UnityEngine;
using System.Collections;

public class HideZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private DemonScript demon; 

    private void OnTriggerStay(Collider c) {
        if (c.CompareTag("Player")){
            Debug.Log("Player entered hide zone.");
            demon.HandleAggressionDecrease();
        }
    }
}
