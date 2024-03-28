using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GunClass equippedGun;
    public Transform gunHold; //where guns are stored on the player


    public void Awake()
    {
        equippedGun = GetComponentInChildren<GunClass>();
    }
    // Method to equip a gun for the player
    public void EquipGun(GunClass gun)
    {
        if (equippedGun != null && gun != equippedGun)
        {
            DropGun();
            
        }

        equippedGun = gun; // Set the equipped gun
        
        

        // Enable gun game object
        equippedGun.gameObject.SetActive(true);

        // Handle any additional logic related to equipping the gun
    }

    // Method to drop the equipped gun
    public void DropGun()
    {
        if (equippedGun != null)
        {
            equippedGun.gameObject.SetActive(false);
            
            // Clear equipped gun reference
            equippedGun = null;
        }
    }

    // Method to switch to a different gun
    public void SwitchGun(GunClass newGun)
    {
        // Drop the currently equipped gun
        DropGun();
        // Equip the new gun
        EquipGun(newGun);
    }

    // Method to handle reloading the equipped gun
    public void ReloadGun()
    {
        if (equippedGun != null)
        {
            equippedGun.Reload();
            // Update UI or perform other actions related to reloading
        }
    }

    // Method to handle shooting with the equipped gun
    public void ShootGun()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
            // Update UI or perform other actions related to shooting
        }
    }

    // Other player-related methods can be added here
}

    // Other player-related methods can be added here


