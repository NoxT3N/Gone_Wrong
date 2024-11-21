using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DemonScript : MonoBehaviour
{
    [Header("Demon Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private int aggro;
    [SerializeField] private LayerMask obstructions;
    [HideInInspector] public bool isPlayerLooking = false;
    private NavMeshAgent agent;

    [Header("Teleport Settings")]
    public float tpInterval = 2f;
    public float minTPdistance = 10f;
    public float maxTPdistance = 20f;
    public float tpAttempts = 3; //number of frames 
    public float edgePadding = 5f;

    private Camera playerCamera;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerCamera = player.GetComponentInChildren<Camera>();
        StartCoroutine(Teleport());        
    }

    void Update()
    {
        if (isPlayerLooking)
        {
            Attack();
        }
    }

    //Teleport and activate demon
    IEnumerator Teleport()
    {
        Debug.Log("Demon teleportation coroutine started");
        while (true)
        {
            yield return new WaitForSecondsRealtime(tpInterval);

            bool wasTPSuccessful = false;

            for (int i = 0; i < tpAttempts; i++)
            {
                Vector3 tpPos = GetRandomPositionAroundPlayer();

                if (isPositionValid(tpPos))
                {
                    agent.Warp(tpPos);
                    wasTPSuccessful = true;
                    Debug.LogError("Demon teleported to: " + tpPos);
                    break;
                }
                else
                {
                    Debug.Log("Demon could not be teleported to: "+ tpPos);
                }
            }

            if (!wasTPSuccessful)
            {
                Debug.LogError("Demon failed to teleport after multiple attempts.");
            }

            yield return null;
        }
    }
    public void Attack()
    {
        Debug.Log("Player is looking at me");
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        float randAngle = UnityEngine.Random.Range(0, 360);
        float randDistance = UnityEngine.Random.Range(minTPdistance, maxTPdistance);
        Vector3 direction = Quaternion.Euler(0, randAngle, 0) * Vector3.forward; //converts angle into direction vector

        //Calculates the teleport position by adding the direction vector pointing to the player multiplied by a random distance
        Vector3 tpPos = player.transform.position + direction * randDistance;

        if (isPositionValid(tpPos))
        {
            return tpPos; // Return a valid position
        }

        Debug.LogWarning("Failed to find a valid teleport position");
        return Vector3.zero; 
    }

    private bool isPositionValid(Vector3 pos)
    {   
        //Converts world position to viewport coordinates
        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(pos);

        //Checks if the random position is whithin the player's FOV
        if (viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1)
        {
            Debug.Log("Position is within the player's FOV: " + pos);
            return false;
        }

        //Checks if the position is on the NavMesh and within the boundaries
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(pos, out hit, 10f, NavMesh.AllAreas))
        {
            Debug.Log("Position is not on the NavMesh: " + pos);
            return false;
        }

        if (Vector3.Distance(hit.position, pos) < edgePadding)
        {
            Debug.Log("Position is too close to the NavMesh edge: " + pos);
            return false;
        }

        Vector3 dirToPos = (pos - player.transform.position).normalized;
    if (Physics.Raycast(player.transform.position, dirToPos, out RaycastHit raycastHit, maxTPdistance, obstructions))
    {
        if (Vector3.Distance(raycastHit.point, pos) < 0.1f)
        {
            return true; // Position is hidden from the player's view
        }
    }

    Debug.Log("Position is visible or invalid: " + pos);
    return false;
    }
}

//First Teleport Attempt 


//Vector3 playerPos = player.transform.position;
//float playerBackDir = player.transform.localEulerAngles.y+180;

////Generate angle from behind player
//int rSide = UnityEngine.Random.Range(0, 2);
//float newAngle = UnityEngine.Random.Range(0.0f, 35.0f);
//if (rSide == 0) newAngle = newAngle * -1;

////Randomise other parts of raycast to go down different ways and
//float xRange = UnityEngine.Random.Range(-0.0f, -10.0f);
//float zRange = UnityEngine.Random.Range(-0.0f, -10.0f);

//Vector3 castDir = new Vector3(xRange, newAngle, zRange);

////Use raycast to find somewhere on the navmesh behind player
//RaycastHit personnel;
//if (Physics.Raycast(playerPos, castDir, out personnel, 20.0f, LayerMask.GetMask("House")))
//{
//    Vector3 behindyou = personnel.point;

//    NavMeshHit hit;
//    if (NavMesh.SamplePosition(behindyou, out hit, 4.0f, NavMesh.AllAreas))
//    {
//        agent.Warp(hit.position);
//        Debug.Log("Demon teleported!");
//        yield return new WaitForSecondsRealtime(2);
//    }
//    else
//    {
//        Debug.Log("Demon failed to find NavMesh around player. Teleportation failed.");
//    }
//}
//else
//{
//    Debug.Log("Demon failed to find a collider behind player. Teleportation failed.");
//}