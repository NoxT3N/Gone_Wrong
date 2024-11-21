using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class DemonScript : MonoBehaviour
{
    [Header("Demon Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private int aggro;
    [HideInInspector] public bool isPlayerLooking = false;
    private NavMeshAgent agent;

    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private VolumeProfile postProfileMain;    

    private Vignette _vignette;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Teleport());    

        postProcessingVolume.profile = postProfileMain;
        postProcessingVolume.profile.TryGet(out _vignette);    
    }

    void Update()
    {
        _vignette.intensity.value = aggro/5f - 0.2f; 
        if (isPlayerLooking)
        {
            IncrementAggression(true);
        }
    }

    //Teleport and activate demon
    IEnumerator Teleport()
    {
        Debug.Log("Demon teleportation coroutine started");
        while (true)
        {
            yield return new WaitForSecondsRealtime(5/aggro);

            Vector3 playerPos = player.transform.position;

            int xSide = UnityEngine.Random.Range(0, 2);
            int zSide = UnityEngine.Random.Range(0, 2);

            float xRange = UnityEngine.Random.Range(0.0f, 4.0f);
            float zRange = UnityEngine.Random.Range(0.0f, 4.0f);

            if (xSide == 0) xRange = xRange * -1;
            if (zSide == 0) zRange = zRange * -1;

            Vector3 randomPos = new Vector3(xRange + playerPos.x, playerPos.y, zRange + playerPos.z);

            //INTRODUCING NEW NAVMESH TECHNOLOGY
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 4.0f, NavMesh.AllAreas))
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


    //Increase or decrease demon aggression every 2 seconds
    public float period = 0.0f;
    void IncrementAggression(Boolean increase)
    {
        if (period > 2)
        {
            Debug.Log("Aggression Level: " + aggro);
            if (increase) {
                if (aggro < 5) aggro++;
                else Debug.Log("Game Over");
            } 
            else {
                if (aggro > 1) aggro--;
            } 
            period = 0;
        }
        
        period += UnityEngine.Time.deltaTime;
    }
}