using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceSlot : MonoBehaviour, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private PuzzlePiece piece;

    [SerializeField]
    private PuzzleManager puzzleManager;

    private CanvasGroup cGroup;

    public void Awake()
    {
        puzzleManager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzzleManager>();
        cGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrop(PointerEventData eData)
    {
        if(eData.pointerDrag != null)
        {
            eData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eData.pointerDrag.GetComponent<PuzzlePiece>().inSpot = true;
            puzzleManager.updatePuzzleState();
        }
    }

    public void OnPointerDown(PointerEventData eData)
    {
        cGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eData)
    {
        cGroup.blocksRaycasts = true;
    }
}
