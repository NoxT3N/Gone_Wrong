using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    CharacterController player;
    const float gravity = -9.81f;
    Vector3 velocity;

    [Header("Player Settings")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    bool isSprinting = false;

    // Awake is called when the script instance is being loaded
    void Awake(){
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update(){
        handleGravity();
        handleMovement();
    }

    private void handleGravity(){
        if (player.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
    }
    private void handleMovement(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        //isSprinting = Input.GetButton("Sprint");

        float currentSpeed = isSprinting ? sprintMultiplier * baseSpeed : baseSpeed;
        Vector3 move = transform.right * x + transform.forward * z;
        player.Move(move * currentSpeed * Time.deltaTime);
        player.Move(velocity * Time.deltaTime);
    }
}
