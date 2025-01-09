using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bewegen : MonoBehaviour
{
    // Rigidbody as variable
    private Rigidbody rb;

    // Movement speed
    public float speed = 5f;
    public float jumpspeed = 20f;

    // Ground check variables
    private bool isGrounded;
    public float groundCheckDistance = 0.1f;  // Adjust as needed for your model
    public LayerMask groundLayer;  // Assign a ground layer in Unity to check only against the ground

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Enable gravity for realistic movement
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Input variables for WASD movement
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Check WASD keys for movement on X and Z axes
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;  // Forward (Z-axis)
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;  // Backward (Z-axis)
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;  // Left (X-axis)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;  // Right (X-axis)
        }

        // Jump logic - only jump if player is grounded
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpspeed, rb.linearVelocity.z);
        }

        // Calculate movement vector (X for left/right, Z for forward/back)
        Vector3 movement = new Vector3(moveHorizontal, rb.linearVelocity.y / speed, moveVertical) * speed;

        // Apply velocity directly to Rigidbody without affecting rotation
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }
}
