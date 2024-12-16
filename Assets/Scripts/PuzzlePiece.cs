using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pData = (PointerEventData)data;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pData.position,
            canvas.worldCamera,
            out pos
            );

        transform.position = canvas.transform.TransformPoint(pos);
    }

}
