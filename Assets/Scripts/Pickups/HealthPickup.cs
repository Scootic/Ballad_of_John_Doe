using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healthAmount = 20;

    private PlayerHealth playerHealth;

    private void Start()
    {
        //find the player's health script
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("Pickups: PlayerHealth script not found in the scene");
        }
    }
    public override void Collect()
    {
        playerHealth.RestoreHealth(healthAmount);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerHealth.Playercurrenthealth < playerHealth.PlayerstartHealth)
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
