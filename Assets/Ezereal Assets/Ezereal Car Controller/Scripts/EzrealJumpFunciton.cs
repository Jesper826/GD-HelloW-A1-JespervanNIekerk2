using UnityEngine;

namespace Ezereal
{
    public class EzerealJumpController : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 1000f; // Force applied to the vehicle when jumping
        [SerializeField] private float jumpCooldown = 1f; // Cooldown time between jumps

        [Header("References")]
        [SerializeField] private Rigidbody vehicleRB; // Reference to the vehicle's Rigidbody

        [Header("Debug")]
        [SerializeField] private bool canJump = true; // Determines if the vehicle can currently jump

        private void Awake()
        {
            if (vehicleRB == null)
            {
                vehicleRB = GetComponent<Rigidbody>();
                if (vehicleRB == null)
                {
                    Debug.LogError("EzerealJumpController: Rigidbody not found! Please assign a Rigidbody to the script.");
                }
            }
        }

        void Update()
        {
            // Check if the V key is pressed and jumping is allowed
            if (Input.GetKeyDown(KeyCode.X) && canJump)
            {
                PerformJump();
            }
        }

        private void PerformJump()
        {
            if (vehicleRB != null)
            {
                vehicleRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void ResetJump()
        {
            canJump = true;
        }
    }
}
