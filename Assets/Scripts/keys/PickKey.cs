using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickKey : MonoBehaviour
{
    public OpenDoor doorScript; // Reference to the OpenDoor script attached to the door
    public GameObject keyGone; // Reference to the key GameObject

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If the player interacts with the key, pick it up
            CollectKey();
        }
    }

    void CollectKey()
    {
        keyGone.SetActive(false); // Deactivate the key GameObject
        doorScript.KeyCollected(); // Inform the door script that a key has been collected
    }
}