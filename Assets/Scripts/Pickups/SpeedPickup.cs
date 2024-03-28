using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    private PlayerMovement playerMovement;

    public bool SpeedIncreased = false;

    [SerializeField] public float newSprintSpeed;

    [SerializeField] public float timer;

    private float OriginalSprintSpeed;
    private void Awake()
    {
        
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.Log("PlayerMovement is not on speedpickup Script");
        }
        OriginalSprintSpeed = playerMovement.sprintSpeed;
    }

    public override void Collect()
    {
        StartCoroutine(IncreaseSpeedTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && SpeedIncreased != true)
        {
            Collect();
        }
    }

    public IEnumerator IncreaseSpeedTimer()
    {
        SpeedIncreased = true;
        playerMovement.sprintSpeed = newSprintSpeed;


        yield return new WaitForSeconds(timer);

        playerMovement.sprintSpeed = OriginalSprintSpeed;
        SpeedIncreased = false;
    }
}
