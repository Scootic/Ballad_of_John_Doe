using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDrop : MonoBehaviour
{
    public GameObject gunDropPrefab; // Prefab for the dropped gun
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.equippedGun != null)
            {
                // Instantiate gun drop prefab at player's position
                gunDropPrefab = Instantiate(gunDropPrefab, player.transform.position, Quaternion.identity);
                // Assign properties of dropped gun based on equipped gun
                // Example: Set ammo count of dropped gun based on equipped gun's ammo count
                // Destroy equipped gun
                Destroy(player.equippedGun.gameObject);
                // Clear equipped gun reference
                player.equippedGun = null;
            }
        }
    }
}
