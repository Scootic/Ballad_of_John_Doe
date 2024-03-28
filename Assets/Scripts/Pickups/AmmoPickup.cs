using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammoAmount = 10;

    private GunClass equippedGun;

    void Start()
    {
        equippedGun = InputManager.instance.equippedGun;
        if (equippedGun == null)
        {
            Debug.Log("No weapon is equipped");
        }
    }
    public override void Collect()
    {
        equippedGun.AddAmmo(ammoAmount);
    }

    public void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player") && equippedGun.CurrentStoredAmmo < equippedGun.MaxStoredAmmo)
        {
            Collect();
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        equippedGun = InputManager.instance.equippedGun;
    }
}
