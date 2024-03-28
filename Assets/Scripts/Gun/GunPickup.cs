using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    
    
    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                GunClass droppedGun = GetComponent<GunClass>(); // Get the gun component of the pickup
                if (droppedGun != null)
                {
                    player.EquipGun(droppedGun); // Equip the picked up gun
                    Destroy(gameObject); // Destroy the pickup object
                }
            }
        }
    }
}

