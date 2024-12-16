using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject intendedSpot;

    [SerializeField]
    private float lockTolerance;

    private RectTransform rect;
    private RectTransform solutionRect;
    private CanvasGroup cGroup;
    public bool inSpot;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        solutionRect = intendedSpot.GetComponent<RectTransform>();
        cGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eData)
    {
        inSpot = false;
        cGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eData)
    {
        rect.anchoredPosition += eData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eData)
    {
        cGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eData)
    {
        Debug.Log("Mouse down");
    }

    /* Old code to be burnt at stake because it was not functional whatsoever
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

    public void DropPiece(BaseEventData data)
    {
        if (!inSpot)
        {
            PointerEventData pData = (PointerEventData)data;
            Vector2 dPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pData.position,
                canvas.worldCamera,
                out dPos
                );

            float distance = Vector2.Distance(dPos, intendedSpot.transform.position);
            Debug.Log(distance, this);
            if (distance <= lockTolerance)
            {
                Debug.Log("Puzzle piece locked in place!");
                inSpot = true;
                Vector2 newPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform)canvas.transform,
                    (Vector2)intendedSpot.transform.position,
                    canvas.worldCamera,
                    out newPos
                    );

                transform.position = canvas.transform.TransformPoint(newPos);
                puzzleManager.updatePuzzleState();
            }
            else
            {
                transform.position = canvas.transform.TransformPoint(dPos);
            }
        }
    }
    */
}
