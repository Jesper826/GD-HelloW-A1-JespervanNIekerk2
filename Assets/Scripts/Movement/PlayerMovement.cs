using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostMultiplier = 2f;
    public KeyCode boostKey = KeyCode.LeftShift;
    public float rotationSpeed = 10f;
    
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    public float jumpspeed = 20f;
    private Rigidbody rb;

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

        if (orientation == null)
            orientation = transform;
    }

    private void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);


        // Capture input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Calculate move direction based on orientation
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Determine speed (normal or boosted)
        float currentSpeed = Input.GetKey(boostKey) ? moveSpeed * boostMultiplier : moveSpeed;

        // Apply movement
        transform.position += moveDirection.normalized * currentSpeed * Time.deltaTime;

        // Handle rotation
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Jump logic - only jump if player is grounded
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpspeed, rb.linearVelocity.z);
        }
    }
}

