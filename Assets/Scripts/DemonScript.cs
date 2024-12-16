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

    void Start()
    {
        StartCoroutine(Teleport());    

        postProcessingVolume.profile = postProfileMain;
        postProcessingVolume.profile.TryGet(out _vignette);    
    }

    void Update()
    {
        _vignette.intensity.value = aggro/5f - 0.2f; 
        if (isPlayerLooking)
        {
            HandleAggressionIncrease();
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

