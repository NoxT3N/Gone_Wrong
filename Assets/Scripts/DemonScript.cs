using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class DemonScript : MonoBehaviour
{
    [Header("Demon Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private int aggro;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            yield return new WaitForSecondsRealtime(2);

            Vector3 playerPos = player.transform.position;

            int xSide = UnityEngine.Random.Range(0, 2);
            int zSide = UnityEngine.Random.Range(0, 2);

            float xRange = UnityEngine.Random.Range(0.0f, 4.0f);
            float zRange = UnityEngine.Random.Range(0.0f, 4.0f);

            if(xSide == 0) xRange = xRange * -1;
            if(zSide == 0) zRange = zRange * -1;

            Vector3 randomPos = new Vector3(xRange+playerPos.x, playerPos.y, zRange+playerPos.z);

            //INTRODUCING NEW NAVMESH TECHNOLOGY
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPos, out hit, 4.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
                Debug.Log("Demon teleported!");
                yield return new WaitForSecondsRealtime(2);
            }
            else
            {
                Debug.Log("Demon failed to find NavMesh around player. Teleportation failed.");
            }
            
        }
    }
}