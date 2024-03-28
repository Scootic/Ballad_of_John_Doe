using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    public List<GameObject> guns = new List<GameObject>();
    private int currentGunIndex = 0;

    

    void Start()
    {
        // Disable all guns except the first one
        for (int i = 1; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            SwitchGun(1);
        }
        else if (scroll < 0f)
        {
            SwitchGun(-1);
        }
    }

    public void SwitchGun(int offset)
    {
        // Disable current gun
        guns[currentGunIndex].SetActive(false);

        // Calculate the index of the next gun
        currentGunIndex = (currentGunIndex + offset + guns.Count) % guns.Count;

        // Enable the next gun
        guns[currentGunIndex].SetActive(true);

        // Notify the InputManager about the weapon switch
        InputManager.instance.UpdateEquippedGun(guns[currentGunIndex].GetComponent<GunClass>());
    }

    // Method to get the currently equipped gun
    public GunClass GetCurrentGun()
    {
        return guns[currentGunIndex].GetComponent<GunClass>();
    }
}

