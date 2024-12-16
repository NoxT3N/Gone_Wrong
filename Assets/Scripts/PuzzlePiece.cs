using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject intendedSpot;

    [SerializeField]
    private float lockTolerance;

    private bool inSpot;

    public void DragPiece(BaseEventData data)
    {
        if (!inSpot)
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

    public void DropPiece()
    {
        float distance = Vector2.Distance(transform.position, intendedSpot.transform.position);
        if(distance <= lockTolerance)
        {
            inSpot = true;
            Vector2 newPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                (Vector2)intendedSpot.transform.position,
                canvas.worldCamera,
                out newPos
                );

            transform.position = canvas.transform.TransformPoint(newPos);
        }
    }
}
