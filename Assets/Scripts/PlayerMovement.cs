using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController player;
    public float speed = 12f;

    const float gravity = -9.81f;
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        if (player.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);
    }
}
