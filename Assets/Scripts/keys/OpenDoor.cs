using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorHinge;
    private bool isLocked = true; // Assuming the door starts locked
    private int keysCollected = 0; // Number of keys collected by the player
    private bool isOpen = false;
    private bool inRange = false;

    // Add references to your audio clips and AudioSource
    public AudioClip openSound;
    public AudioClip lockedSound;
    private AudioSource audioSource;

    void Start()
    {
        isOpen = false;
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Log an error if no AudioSource is found
            Debug.LogError("No AudioSource component found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOpen == false && inRange == true)
        {
            if (!isLocked)
            {
                doorHinge.Play();
                isOpen = true;
                // Play open sound effect
                if (openSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(openSound);
                }
            }
            else
            {
                Debug.Log("The door is locked. You need a key to unlock it.");
                
                // Play locked sound effect
                if (lockedSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(lockedSound);
                }
            }
        }
    }

    // Method to unlock the door
    public void UnlockDoor()
    {
        isLocked = false;
    }

    // Method to inform the door script that a key has been collected
    public void KeyCollected()
    {
        keysCollected++;
        if (keysCollected >= 2) // Check if the player has collected enough keys to unlock the door
        {
            UnlockDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = false;
        }
    }
}
