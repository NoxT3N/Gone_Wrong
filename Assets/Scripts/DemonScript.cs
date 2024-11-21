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
            float playerBackDir = player.transform.localEulerAngles.y+180;

            //Generate angle from behind player
            int rSide = UnityEngine.Random.Range(0, 2);
            float newAngle = UnityEngine.Random.Range(0.0f, 35.0f);
            if (rSide == 0) newAngle = newAngle * -1;

            //Randomise other parts of raycast to go down different ways and
            float xRange = UnityEngine.Random.Range(-0.0f, -10.0f);
            float zRange = UnityEngine.Random.Range(-0.0f, -10.0f);

            Vector3 castDir = new Vector3(xRange, newAngle, zRange);

            //Use raycast to find somewhere on the navmesh behind player
            RaycastHit personnel;
            if (Physics.Raycast(playerPos, castDir, out personnel, 20.0f, LayerMask.GetMask("House")))
            {
                Vector3 behindyou = personnel.point;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(behindyou, out hit, 4.0f, NavMesh.AllAreas))
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
            else
            {
                Debug.Log("Demon failed to find a collider behind player. Teleportation failed.");
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