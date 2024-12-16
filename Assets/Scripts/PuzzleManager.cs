using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public int lockedPieces = 0;

    public void updatePuzzleState()
    {
        Debug.Log("wah");
        lockedPieces++;

        if(lockedPieces==6)
        {
            Debug.Log("A winner is you! Barbie is kil! Yes!");
        }
    }

}
