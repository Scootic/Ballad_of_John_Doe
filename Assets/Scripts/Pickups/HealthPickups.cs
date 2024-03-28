using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupTypes { SMALLHEALTH, MEDIUMHEALTH, LARGEHEALTH }
public class HealthPickups : MonoBehaviour
{
    public PickupTypes pickup;

    public float myValue;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerHealth.Playercurrenthealth < playerHealth.PlayerstartHealth)
        {
            switch (pickup)
            {
                case PickupTypes.SMALLHEALTH:
                    playerHealth.RestoreHealth(myValue);
                    break;
                case PickupTypes.MEDIUMHEALTH:
                    playerHealth.RestoreHealth(myValue);
                    break;
                case PickupTypes.LARGEHEALTH:
                    playerHealth.RestoreHealth(myValue);
                    break;

            }

            Destroy(gameObject);

        }
    }


}
