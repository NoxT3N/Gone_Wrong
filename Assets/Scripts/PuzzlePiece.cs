using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject intendedSpot;

    [SerializeField]
    private float lockTolerance;

    [SerializeField]
    private PuzzleManager puzzleManager;

    public bool inSpot;

    private void Start()
    {
        puzzleManager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzzleManager>();
    }

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
}
