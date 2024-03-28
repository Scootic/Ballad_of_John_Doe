using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 649

    // CharacterController component for player movement
    [SerializeField] CharacterController controller;

    // Movement speed variables
    [SerializeField] float speed = 11f;
    [SerializeField] public float sprintSpeed = 17f;
    [SerializeField] public float IncreasedSprintSpeed = 25f;
    [SerializeField] float sprintAcceleration = 5f;

    // Variables for handling slopes
    [SerializeField] float slopeForce = 5f;
    [SerializeField] float slopeRayLength = 0.5f;

    // Variables for wall running
    [SerializeField] float wallRunForce = 5f;
    [SerializeField] float wallRunHeightOffset = 2f;

    // Capsule dimensions for grounding check
    [SerializeField] float capsuleHeight = 2f;
    [SerializeField] float capsuleRadius = 0.5f;

    // Input variables
    Vector2 horizontalInput;

    // Jumping variables
    [SerializeField] float jumpHeight = 3.5f;
    [SerializeField] float gravity = -30f;
    [SerializeField] float groundedRadius = 0.2f;
    [SerializeField] float jumpGroundedRadius = 0.8f;
    Vector3 verticalVelocity = Vector3.zero;

    // Ground check using layer mask
    [SerializeField] LayerMask groundMask;
    [SerializeField] public bool isGrounded; // Serialized for debug

    // Jumping control variables
    bool canJump;
    bool jump;
    [SerializeField] int jumpAmount = 1;
    [SerializeField] int currentJump = 0; // Serialized for debug

    // Sprinting control variables
    public bool sprint;

    
    // Update is called once per frame
    private void Update()
    {
        HandleMovementInput();
    }

    // Method to handle player movement input
    void HandleMovementInput()
    {
        // Check if the player can jump based on a downward raycast
        canJump = Physics.Raycast(transform.position, Vector3.down, jumpGroundedRadius, groundMask);
        if (canJump)
        {
            currentJump = 0;
        }
        else
        {
            if (currentJump == 0)
            {
                currentJump += 1;
            }
        }

        // Check grounding with capsule cast
        isGrounded = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * capsuleHeight, capsuleRadius, Vector3.down, groundedRadius, groundMask);
        if (isGrounded)
        {
            if (verticalVelocity.y < 0)
            {
                verticalVelocity.y = 0;
            }
        }
        else
        {
            // Apply gravity when not grounded
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        // Determine current speed based on sprinting status
        float currentSpeed = sprint ? sprintSpeed : speed;
        // Determine target speed based on sprinting status
        float targetSpeed = sprint ? sprintSpeed : speed;

        // Lerp the current speed towards the target speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, sprintAcceleration * Time.deltaTime);
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * currentSpeed;

        
        

        
        // Check for slope
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, slopeRayLength, groundMask))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (slopeAngle > controller.slopeLimit)
            {
                // Apply slope force to slide down
                verticalVelocity += Vector3.down * slopeForce * Time.deltaTime;

                // Add wall run force only while sprinting
                if (slopeAngle < 90f)
                {
                    // Calculate the direction of the wall run force
                    Vector3 wallRunForceDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;

                    // Apply wall run force
                    controller.Move(wallRunForceDirection * wallRunForce * Time.deltaTime);

                    // Adjust vertical position to lose height while running across the wall
                    Vector3 wallRunHeightPosition = transform.position;
                    wallRunHeightPosition.y = Mathf.Lerp(wallRunHeightPosition.y, hit.point.y - wallRunHeightOffset, 0.1f);
                    transform.position = wallRunHeightPosition;
                }
            }
        }

        // Move the player horizontally
        controller.Move(horizontalVelocity * Time.deltaTime);

        // Handle jumping
        if (jump)
        {
            if (currentJump < jumpAmount)
            {
                // Set vertical velocity for jumping
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                currentJump += 1;
            }

            jump = false;
        }

        // Move the player vertically (apply gravity)
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    // Method to receive horizontal input from an external source
    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    // Method to handle sprinting input
    public void OnSprintPressed()
    {
        // Toggle sprinting on or off
        sprint = !sprint;
    }

    // Method to handle jump input
    public void OnJumpPressed()
    {
        jump = true;
    }

    public void increaseJumpAmount(int amount)
    {
        jumpAmount += amount;
    }
}
