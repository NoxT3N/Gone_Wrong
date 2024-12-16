using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    Vector3 mouseOffset;

    private Vector3 mousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    //Called on first frame piece is clicked
    private void OnMouseDown()
    {
        mouseOffset = Input.mousePosition - mousePos();
    }

    //Called while piece is being dragged
    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mouseOffset);
    }

}
