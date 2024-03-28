using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    public float movementDistance = 1f; // Distance to move up and down
    public float movementSpeed = 1f; // Speed of movement

    private Vector3 startPos;
    private float direction = 1f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the new position
        float newY = Mathf.PingPong(Time.time * movementSpeed, movementDistance) * direction;
        Vector3 newPos = startPos + Vector3.up * newY;

        // Update the position
        transform.position = newPos;
    }

    // Optionally, you can add a method to change the direction of movement
    public void ReverseDirection()
    {
        direction *= -1f;
    }
}
