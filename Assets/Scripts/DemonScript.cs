using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DemonScript : MonoBehaviour
{
    [Header("Demon Settings")]
    [SerializeField] private GameObject player;
    public int aggro;
    [HideInInspector] public bool isPlayerLooking = false;
    private NavMeshAgent agent;

    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private VolumeProfile postProfileMain;    

    private Vignette _vignette;
    private int lookCount = 0; // Track how many times the player looks at the demon and the number of looks before a game over
    private const int maxLooks = 5;
    private bool hasLooked = false;

    //Vignette settings
    private float minIntensity = 0.0f;
    private float maxIntensity = 1.0f;
    private float intensityStep = 0.15f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Teleport());    

        postProcessingVolume.profile = postProfileMain;
        if (!postProcessingVolume.profile.TryGet(out _vignette))
        {
            Debug.LogError("Vignette not found in the Volume Profile.");
        }
        else
        {
            _vignette.intensity.value = minIntensity;
        }
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        //_vignette.intensity.value = aggro/5f - 0.2f;
        if (isPlayerLooking && !hasLooked)
        {
            //IncrementAggression(true);
            HandleAggressionIncrease();
            HandleLookCount();
            UpdateVignette();
            hasLooked = true;
        }
        else if (!isPlayerLooking) 
        {
            hasLooked = false;
        }
    }

    private void UpdateVignette()
    {
        if (_vignette != null)
        {
            _vignette.intensity.value = Mathf.Clamp(_vignette.intensity.value + intensityStep, minIntensity, maxIntensity);
        }
    }

    private void HandleLookCount() 
    {
        if (isPlayerLooking) 
        {
            lookCount++;
            if (lookCount > maxLooks)//triggers game over
            {
                FindObjectOfType<PauseMenu>().ShowGameOver();
                Debug.Log("Player looked at the demon too many times");
            }
            else 
            {
                Debug.Log($"Player has looked at the demon {lookCount} times.");
            }

            //isPlayerLooking = false;
        }
    }

    //Teleport and activate demon
    IEnumerator Teleport()
    {
        Debug.Log("Demon teleportation coroutine started");
        while (true)
        {

            // Wait until the game is not paused
            while (PauseMenu.isPaused)
            {
                yield return null; // Pause the coroutine execution
            }

            yield return new WaitForSecondsRealtime(5/aggro);

            Vector3 playerPos = player.transform.position;

            int xSide = UnityEngine.Random.Range(0, 2);
            int zSide = UnityEngine.Random.Range(0, 2);

            float xRange = UnityEngine.Random.Range(0.0f, 4.0f);
            float zRange = UnityEngine.Random.Range(0.0f, 4.0f);

            if (xSide == 0) xRange = xRange * -1;
            if (zSide == 0) zRange = zRange * -1;

            Vector3 randomPos = new Vector3(xRange + playerPos.x, 1.149f, zRange + playerPos.z);

            //INTRODUCING NEW NAVMESH TECHNOLOGY
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 4.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
                Debug.Log("Demon teleported!");

                // Wait again until the game is not paused
                while (PauseMenu.isPaused)
                {
                    yield return null;
                }

                yield return new WaitForSecondsRealtime(2);
            }
            else
            {
                Debug.Log("Demon failed to find NavMesh around player. Teleportation failed.");
            }

        }
    }


    //Increase or decrease demon aggression every 2 seconds
    //public float period = 0.0f;
    //public void IncrementAggression(Boolean increase)
    //{
    //    if (period > 2)
    //    {
    //        Debug.Log("Aggression Level: " + aggro);
    //        if (increase) {
    //            if (aggro < 5) aggro++;
    //            else Debug.Log("Game Over");
    //        } 
    //        else {

    //            if (aggro > 1) aggro--;
    //        } 
    //        period = 0;
    //    }

    //    period += UnityEngine.Time.deltaTime;
    //}

    private float increaseTimer = 0f;
    private void HandleAggressionIncrease()
    {
        increaseTimer += Time.deltaTime;
        if (increaseTimer >= 2f)
        {
            if (aggro < 5)
            {
                aggro++;
                Debug.Log("Aggression increased. Level: " + aggro);
            }
            else
            {
                Debug.Log("Game Over");
            }
            increaseTimer = 0f;
        }
    }
    private float decreaseTimer = 0f;
    public void HandleAggressionDecrease()
    {
        decreaseTimer += Time.deltaTime;
        if (decreaseTimer >= 2f)
        {
            if (aggro > 1)
            {
                aggro--;
                Debug.Log("Aggression decreased. Level: " + aggro);
            }
            decreaseTimer = 0f;
        }
    }
}