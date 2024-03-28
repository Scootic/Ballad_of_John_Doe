using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorLocked = true;
    private bool playerHasKey = false;

    public void UnlockDoor()
    {
        doorLocked = false;
        // Code to open the door, e.g., rotate it, move it, etc.
    }

    private void Update()
    {
        if (!doorLocked || playerHasKey)
        {
            // Code to open the door
            // For demonstration purposes, let's assume the door rotates when opened
            if (!doorLocked)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 90f); // Rotate the door
            }
        }
    }

    // Method to be called when the player picks up the key
    public void PlayerPicksUpKey()
    {
        playerHasKey = true;
    }
}