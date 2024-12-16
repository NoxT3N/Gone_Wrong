using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData  eData)
    {
        if(eData.pointerDrag != null)
        {
            eData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
