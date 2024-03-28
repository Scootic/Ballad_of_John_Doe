using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class AmmoUpgrade : Pickup
{
    
    public int myValue;
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
        equippedGun.AmmoUpgrade(myValue);
        equippedGun.AddAmmo(myValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        equippedGun = InputManager.instance.equippedGun;
    }
}
