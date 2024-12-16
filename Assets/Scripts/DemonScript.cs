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
    [SerializeField] private Transform[] tpPoints;
    [SerializeField] private float tpDelay = 5f;
    [SerializeField] private float maxTPdistance;
    public int aggro;
    [HideInInspector] public bool isPlayerLooking = false;

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
            yield return new WaitForSecondsRealtime(tpDelay / aggro);

            if (!isPlayerLooking)
            {
                foreach (Transform tpPoint in tpPoints)
                {
                    Vector3 playerPos = player.transform.position;

            // Wait until the game is not paused
            while (PauseMenu.isPaused)
            {
                yield return null; // Pause the coroutine execution
            }
                    //Check distance to ensure it's within proximity
                    Vector3 dirToPoint = (tpPoint.position - playerPos).normalized;
                    float distToPoint = Vector3.Distance(playerPos, tpPoint.position);

                    //Check angle to ensure it's outside player's FOV
                    float angleToPoint = Vector3.Angle(player.transform.forward, dirToPoint);

                    if (distToPoint <= maxTPdistance && angleToPoint > 90f / 2f)
                    {
                        this.gameObject.transform.position = tpPoint.position;
                        Debug.Log($"Demon teleported to: {tpPoint.position}");
                        break; 
                    }
                }
            }
        }
    }




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

