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
    public float jumpSpeed = 200f;
    public float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        if (orientation == null)
            orientation = transform;
    }

    void Update()
    {
        // Ground detection using a raycast
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance);

        // Visualize the raycast in the editor for debugging
        Color rayColor = isGrounded ? Color.red : Color.green;
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, rayColor);

        // Capture input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        // Calculate move direction based on orientation
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Determine speed (normal or boosted)
        float currentSpeed = Input.GetKey(boostKey) ? moveSpeed * boostMultiplier : moveSpeed;

        // Apply movement
        Vector3 newPosition = transform.position + moveDirection.normalized * currentSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Handle rotation
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }

        // Handle jump input
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(rb.linearVelocity);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

        }
    }
}
