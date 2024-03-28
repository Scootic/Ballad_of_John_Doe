using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour

{
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI currentStoredAmmoText;

    private GunClass equippedGun;

    void Start()
    {
        // Ensure the UI elements are assigned in the Inspector
        if (currentAmmoText == null || currentStoredAmmoText == null)
        {
            Debug.LogError("Need to assign the currentAmmoText and currentStoredAmmoText in the inspector");
            return;
        }

        // Get the initial equipped gun from the InputManager
        equippedGun = InputManager.instance.equippedGun;
        if (equippedGun == null)
        {
            Debug.LogError("No equipped gun found!");
            return;
        }

        // Update UI with initial gun's ammo
        UpdateAmmoUI();
    }

    void Update()
    {
        // Check if the equipped gun has changed and update UI if necessary
        if (equippedGun != InputManager.instance.equippedGun)
        {
            equippedGun = InputManager.instance.equippedGun;
            UpdateAmmoUI();
        }
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        // Update UI with current and stored ammo of the equipped gun
        if (equippedGun != null)
        {
            currentAmmoText.text = "Current Ammo: " + equippedGun.CurrentBullets.ToString();
            currentStoredAmmoText.text = "Stored Ammo: " + equippedGun.CurrentStoredAmmo.ToString();
        }
        else
        {
            // Clear UI if no gun is equipped
            currentAmmoText.text = "Current Ammo: -";
            currentStoredAmmoText.text = "Stored Ammo: -";
        }
    }
}


/*{
    [SerializeField] private TextMeshProUGUI CurrentAmmoText;
    [SerializeField] private TextMeshProUGUI CurrentStoredAmmoText;

    private GunClass activeGun;

    private void Start()
    {
        if (CurrentAmmoText == null || CurrentStoredAmmoText == null)
        {
            Debug.LogError("Need to assign the currentAmmo text and CurrentStoredAmmoText in the inspector");
            return;
        }

        activeGun = FindObjectOfType<GunClass>();
        if (activeGun == null)
        {
            Debug.LogError("Can't find gun in scene!");
            return;
        }
        UpdateCurrentAmmo(activeGun.CurrentBullets);
        UpdateCurrentStoredAmmo(activeGun.CurrentStoredAmmo);
    }

    private void Update()
    {
        UpdateCurrentAmmo(activeGun.CurrentBullets);
        UpdateCurrentStoredAmmo(activeGun.CurrentStoredAmmo);
    }

    void UpdateCurrentAmmo(int currentAmmo)
    {
        CurrentAmmoText.text = "Current Ammo: " + currentAmmo.ToString();
    }

    void UpdateCurrentStoredAmmo(int currentStoredAmmo)
    {
        CurrentStoredAmmoText.text = "Stored Ammo: " + currentStoredAmmo.ToString();
    }
}*/
