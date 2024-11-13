using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DemonScript : MonoBehaviour
{
    [Header("Demon Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private int aggro;

    void Start()
    {
        //setVisible(isVisible);
        StartCoroutine(Teleport());
    }


    //Hides demon + disables collision
    //void toggleVisible()
    //{
    //    isVisible = !isVisible;
    //    demonhider.enabled = isVisible;
    //}
    //void setVisible(bool visible)
    //{
    //    demonhider.enabled = visible;
    //}

    //Teleport and activate demon
    IEnumerator Teleport()
    {
        Debug.Log("Demon teleportation coroutine started");
        while (true)
        {
            yield return new WaitForSecondsRealtime(20);

            Vector3 playerPos = player.transform.position;
            Vector3 newPos = new Vector3();

            newPos.x = playerPos.x + Random.Range(-20.0f, 20.0f);
            newPos.y = playerPos.y;
            newPos.x = playerPos.x + Random.Range(-20.0f, 20.0f);

            this.transform.position = newPos;
            //toggleVisible();
            Debug.Log("Demon appeared!");
            yield return new WaitForSecondsRealtime(5);
            //toggleVisible();
            Debug.Log("Demon is gone.");
        }
    }
}
