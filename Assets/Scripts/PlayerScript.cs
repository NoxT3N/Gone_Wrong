using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    CharacterController player;
    const float gravity = -9.81f;
    Vector3 velocity;

    [Header("Player Settings")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private Camera POV;
    [SerializeField] private float lookDistance = 50f;

    private bool isSprinting = false;
    private bool isMoving = false; // Track if the player is moving


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    void Start()
    {
        Time.timeScale = 1.0f; 
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            HandleGravity();
            HandleMovement();
            PerformRaycast();
        }
        else 
        {
            if (isMoving) 
            {
                AudioManager.Instance.Stop("Walk");
                isMoving = false;
            }
        }
    }

    private void HandleGravity()
    {
        if (player.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
    }
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        //isSprinting = Input.GetButton("Sprint");

        float currentSpeed = isSprinting ? sprintMultiplier * baseSpeed : baseSpeed;
        Vector3 move = transform.right * x + transform.forward * z;
        player.Move(move * currentSpeed * Time.deltaTime);
        player.Move(velocity * Time.deltaTime);

        if (move.magnitude > 0.1f) // Check if the player is moving
        {
            if (!isMoving) 
            {
                // Play the walking sound if not already playing
                AudioManager.Instance.Play("Walk");
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                // Stop the walking sound when the player stops
                AudioManager.Instance.Stop("Walk");
                isMoving = false;
            }
        }


    }
    private void DetectDemon(RaycastHit hit)
    {
            DemonScript demon = hit.collider.GetComponent<DemonScript>();
            GameManager.Instance.SetIsPlayerLookingAtDemon(demon != null);
    }
    private void HandleItemInteractions(RaycastHit hit)
    {
        Item item = hit.collider.GetComponent<Item>();
        if (item != null)
        {
            if (GameManager.Instance.CanInteract(item))
            {
                GameManager.Instance.OutlineItem(item);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Debug.Log("E key pressed");
                    item.Interact();
                }
            }
            //if (GameManager.Instance.CanInteract(item) && Input.GetKeyDown(KeyCode.E))
            //{
            //    item.Interact();
            //}

        }
        else
        {
            GameManager.Instance.ClearOutline();
        }
    }
    private void PerformRaycast()
    {
        if (POV == null)
        {
            Debug.LogError("POV Camera is not assigned.");
            return;
        }

        RaycastHit hit;
        Vector3 rayOrigin = POV.transform.position;
        Vector3 rayDirection = POV.transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * lookDistance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, lookDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            DetectDemon(hit);
            HandleItemInteractions(hit);
        }
        else
        {
            GameManager.Instance.SetIsPlayerLookingAtDemon(false);
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
